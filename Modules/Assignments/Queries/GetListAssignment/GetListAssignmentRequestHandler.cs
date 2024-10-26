using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.DTOs.Assignments;
using demo_tt.Entities.Constants;
using demo_tt.Repositories.IRepositories;

namespace demo_tt.Modules.Assignments.Queries.GetListAssignment
{
    public class GetListAssignmentRequestHandler : IRequestHandler<GetListAssignmentRequest, GetListAssignmentResponse>
    {
        private readonly IAssignmentsRepository _assignmentsRepository;
        private readonly ICacheRepository _cacheRepository;
        public GetListAssignmentRequestHandler(IAssignmentsRepository assignmentsRepository,
                                                ICacheRepository cacheRepository)
        {
            _assignmentsRepository = assignmentsRepository;
            _cacheRepository = cacheRepository;
        }

        public async Task<GetListAssignmentResponse> Handle(GetListAssignmentRequest request, CancellationToken cancellationToken)
        {
            GetListAssignmentResponse result = new GetListAssignmentResponse();
            var existCache = await _cacheRepository.GetCacheData<List<AssignmentDTOs>>(ConstantsEntity.REDIS_ASSIGNMENTS);
            if (existCache is not null && existCache.Count > 0)
            {
                result.Assignments = existCache;
            }
            else
            {
                var existAssignments = await _assignmentsRepository.GetListAssignmentsAsync(ConstantsEntity.NOT_DELETED);
                if (existAssignments is not null && existAssignments.Count > 0)
                {
                    result.Assignments.AddRange(existAssignments.Select(x => new AssignmentDTOs
                    {
                        TruckID = x.Trucks.TruckID,
                        AreaID = x.AffectedAreas.AreaID,
                        ResourcesDelivered = x.AffectedAreas.ResourceAffectedAreas.Select(r => new AssignmentResourceDTOs
                        {
                            Quantity = r.Quantity,
                            Name = r.MasterResource.Name
                        }).ToList(),
                    }));

                    await _cacheRepository.SetCacheData<List<AssignmentDTOs>>(ConstantsEntity.REDIS_ASSIGNMENTS, result.Assignments, DateTimeOffset.Now.AddMinutes(ConstantsEntity.MAX_TIME_REDIS_MIN));
                }
            }

            return result;
        }
    }
}