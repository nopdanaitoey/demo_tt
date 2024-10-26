

using demo_tt.DTOs.Trucks;
using demo_tt.Entities;
using demo_tt.Entities.Constants;
using demo_tt.Repositories.IRepositories;

namespace demo_tt.Modules.Trucks.Commands.CreateTruck
{
    public class CreateTruckRequestHandler : IRequestHandler<CreateTruckRequest, CreateTruckResponse>
    {
        private readonly ITrucksRepository _trucksRepository;
        private readonly IAffectedAreasRepository _affectedAreasRepository;
        private readonly IMasterResourceRepository _masterResourceRepository;
        private readonly ICacheRepository _cacheRepository;
        public CreateTruckRequestHandler(ITrucksRepository trucksRepository,
        ICacheRepository cacheRepository,
        IMasterResourceRepository masterResourceRepository,
        IAffectedAreasRepository affectedAreasRepository)
        {
            _trucksRepository = trucksRepository;
            _cacheRepository = cacheRepository;
            _masterResourceRepository = masterResourceRepository;
            _affectedAreasRepository = affectedAreasRepository;

        }

        public async Task<CreateTruckResponse> Handle(CreateTruckRequest request, CancellationToken cancellationToken)
        {
           
            CreateTruckResponse result = new CreateTruckResponse();
            var existTruck = await _trucksRepository.GetByTruckIDAsync(request.TruckID);
            if (existTruck is not null) throw new Exception("Truck already exist");
            if (request.Resources is null || request.Resources.Count == 0) throw new Exception("Resource not found");

            var existAffectedAreas = await _cacheRepository.GetCacheData<List<Entities.AffectedAreas>>(ConstantsEntity.REDIS_AFFECTED_AREAS);
            if (existAffectedAreas is null || existAffectedAreas.Count == 0) throw new Exception("Affected Areas not found");

            Guid newTruckID = Guid.NewGuid();
            Entities.Trucks trucks = new Entities.Trucks
            {
                Id = newTruckID,
                TruckID = request.TruckID,
                IsActive = ConstantsEntity.ACTIVE
            };


            var existMasterResource = await _cacheRepository.GetCacheData<List<Entities.MasterResource>>(ConstantsEntity.REDIS_MASTER_RESOURCE);
            if (existMasterResource is null || existMasterResource.Count == 0) throw new Exception("Master Resource not found");

            if (request.Resources.Any(x => !existMasterResource.Any(y => y.Id == x.ResourceID))) throw new Exception("Resource not found");
            trucks.ResourceTrucks = new List<ResourceTrucks>();
            trucks.TravelTimeToAreas = new List<TravelTimeToAreas>();
            foreach (var resource in request.Resources)
            {

                trucks.ResourceTrucks.Add(new ResourceTrucks
                {
                    Id = Guid.NewGuid(),
                    TruckID = newTruckID,
                    ResourceID = resource.ResourceID,
                    Quantity = resource.Quantity,
                    AvailableQuantity = resource.Quantity,
                    IsActive = ConstantsEntity.ACTIVE

                });


            }

            foreach (var time in request.TimeTruckToAreas)
            {

                trucks.TravelTimeToAreas.Add(new TravelTimeToAreas
                {
                    AreaID = time.AreaID,
                    TruckID = newTruckID,
                    TravelTime = time.TravelTime,
                });

            }


            var resultAdd = await _trucksRepository.Add(trucks, default);
            result.IsSuccess = resultAdd is not null;
            result.Message = result.IsSuccess ? "Success" : "Failed";


            if (result.IsSuccess is true && resultAdd is not null)
            {
                var existCache = await _cacheRepository.GetCacheData<List<Entities.Trucks>>(ConstantsEntity.REDIS_TRUCKS);
                if (existCache is null || existCache.Count <= 0) existCache = new List<Entities.Trucks>();

                existCache.Add(resultAdd);

                await _cacheRepository.SetCacheData<List<Entities.Trucks>>(ConstantsEntity.REDIS_TRUCKS, existCache, DateTimeOffset.Now.AddMinutes(ConstantsEntity.MAX_TIME_REDIS_MIN));
            }

            return result;
        }
    }
}