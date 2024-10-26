
using demo_tt.DTOs.Assignments;
using demo_tt.Entities.Constants;

using demo_tt.Repositories.IRepositories;


namespace demo_tt.Modules.Assignments.Commands.CreateAssignment
{
    public class CreateAssignmentRequestHandler : IRequestHandler<CreateAssignmentRequest, CreateAssignmentResponse>
    {
        private readonly IAffectedAreasRepository _affectedAreasRepository;
        private readonly ITrucksRepository _trucksRepository;
        private readonly IAssignmentsRepository _assignmentsRepository;
        private readonly ICacheRepository _cacheRepository;
        public CreateAssignmentRequestHandler(IAffectedAreasRepository affectedAreasRepository,
            ITrucksRepository trucksRepository,
            IAssignmentsRepository assignmentsRepository,
            ICacheRepository cacheRepository)
        {
            _affectedAreasRepository = affectedAreasRepository;
            _trucksRepository = trucksRepository;
            _assignmentsRepository = assignmentsRepository;
            _cacheRepository = cacheRepository;

        }

        public async Task<CreateAssignmentResponse> Handle(CreateAssignmentRequest request, CancellationToken cancellationToken)
        {
            CreateAssignmentResponse result = new CreateAssignmentResponse();
            result.Assignments = new List<AssignmentDTOs>();
            List<Entities.AffectedAreas> existAffectedAreas = new List<Entities.AffectedAreas>();
            var existCacheAreas = await _cacheRepository.GetCacheData<List<Entities.AffectedAreas>>(ConstantsEntity.REDIS_AFFECTED_AREAS);
            if (existCacheAreas is not null && existCacheAreas.Count > 0)
            {
                existAffectedAreas = existCacheAreas
                                                      .Where(x => x.IsActive == ConstantsEntity.ACTIVE
                                && x.Assignments is null || x.Assignments.Where(a => a.IsActive == ConstantsEntity.ACTIVE
                                && a.IsDeleted == ConstantsEntity.NOT_DELETED).Count() == 0
                                ).OrderByDescending(x => x.UrgencyLevel).ToList();
            }

            if (existAffectedAreas is null || existAffectedAreas.Count == 0) throw new Exception("Affected Areas not found");
            List<Entities.Trucks> existTrucks = new List<Entities.Trucks>();
            var existCacheTrucks = await _cacheRepository.GetCacheData<List<Entities.Trucks>>(ConstantsEntity.REDIS_TRUCKS);
            if (existCacheTrucks is not null && existCacheTrucks.Count > 0)
            {
                existTrucks = existCacheTrucks.Where(x => x.IsActive == ConstantsEntity.ACTIVE
                                    && x.ResourceTrucks is not null && x.ResourceTrucks.Where(r => r.IsActive == ConstantsEntity.ACTIVE && r.AvailableQuantity > 0).Count() > 0
                                    && x.Assignments is null || (x.Assignments.Where(x => x.IsDeleted == ConstantsEntity.NOT_DELETED).ToList().Count == 0))
                                    .ToList();
            }

            if (existTrucks is null || existTrucks.Count == 0) throw new Exception("Trucks not found");

            List<Entities.Assignments> assignmentsList = new List<Entities.Assignments>();

            foreach (var area in existAffectedAreas)
            {

                var matchTruckList = existTrucks
                                .Where(x =>
                                x.TravelTimeToAreas.Any(t => t.AreaID == area.Id && t.IsActive == ConstantsEntity.ACTIVE)
                                && x.ResourceTrucks
                                .Where(r => area.ResourceAffectedAreas
                                .Any(a => a.ResourceID == r.ResourceID && r.AvailableQuantity >= a.Quantity && a.IsActive == ConstantsEntity.ACTIVE)).Count() > 0)
                                .ToList();

                if (matchTruckList is not null && matchTruckList.Count > 0)
                {


                    var matchTruck = matchTruckList
                                    .Where(x => x.ResourceTrucks.Where(r => r.IsActive == ConstantsEntity.ACTIVE && area.ResourceAffectedAreas.Where(a => a.ResourceID == r.ResourceID).Count() > 0).Count() == area.ResourceAffectedAreas.Where(x => x.IsActive == ConstantsEntity.ACTIVE).Count()).MinBy(x => x.TravelTimeToAreas.Min(y => y.TravelTime));
                    if (matchTruck is not null)
                    {
                        assignmentsList.Add(new Entities.Assignments
                        {
                            Id = Guid.NewGuid(),
                            AreaID = area.Id,
                            TruckID = matchTruck.Id,
                            IsActive = ConstantsEntity.ACTIVE,
                            IsDeleted = ConstantsEntity.NOT_DELETED,
                            AffectedAreas = area,
                            Trucks = matchTruck
                        });
                        result.Assignments.Add(new AssignmentDTOs
                        {
                            TruckID = matchTruck.TruckID,
                            AreaID = area.AreaID,
                            ResourcesDelivered = area.ResourceAffectedAreas.Select(x => new AssignmentResourceDTOs
                            {
                                Quantity = x.Quantity,
                                Name = x.MasterResource.Name
                            }).ToList()
                        });
                        existTrucks.Remove(matchTruck);
                    }
                }

            }

            if (assignmentsList.Count > 0)
            {
                var createAssignmentModel = assignmentsList.Select(x => new Entities.Assignments
                {
                    AreaID = x.AreaID,
                    TruckID = x.TruckID,
                }).ToList();
                var createdAssignments = await _assignmentsRepository.AddRage(createAssignmentModel, cancellationToken);
                result.IsSuccess = createdAssignments.Count > 0;
                result.Message = result.IsSuccess is true ? "Success" : "Fail";
                if (result.IsSuccess is true)
                {
                    var existCache = await _cacheRepository.GetCacheData<List<AssignmentDTOs>>(ConstantsEntity.REDIS_ASSIGNMENTS);
                    if (existCache is not null && existCache.Count > 0)
                    {
                        existCache.AddRange(result.Assignments);

                    }
                    else
                    {
                        existCache = result.Assignments;
                    }
                    await _cacheRepository.SetCacheData(ConstantsEntity.REDIS_ASSIGNMENTS, existCache, DateTimeOffset.Now.AddMinutes(ConstantsEntity.MAX_TIME_REDIS_MIN));

                }

            }
            result.IsSuccess = true;
            result.Message = "Success";





            return result;
        }
    }
}