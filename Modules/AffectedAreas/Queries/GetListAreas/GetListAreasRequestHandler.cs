using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Entities.Constants;
using demo_tt.Repositories.IRepositories;

namespace demo_tt.Modules.AffectedAreas.Queries.GetListAreas
{
    public class GetListAreasRequestHandler : IRequestHandler<GetListAreasRequest, GetListAreasResponse>
    {
        private readonly ICacheRepository _cacheRepository;

        public GetListAreasRequestHandler(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<GetListAreasResponse> Handle(GetListAreasRequest request, CancellationToken cancellationToken)
        {
            GetListAreasResponse result = new GetListAreasResponse();
            var existCache = await _cacheRepository.GetCacheData<List<Entities.AffectedAreas>>(ConstantsEntity.REDIS_AFFECTED_AREAS);
            
            if (existCache is not null && existCache.Count > 0)
            {
                result.AffectedAreas = new List<DTOs.AffectedAreas.AffectedAreasDTOs>();
                result.AffectedAreas = existCache.Where(x => x.IsActive == ConstantsEntity.ACTIVE)
                                    .Select(x => new DTOs.AffectedAreas.AffectedAreasDTOs
                                    {
                                        Id = x.Id,
                                        AreaID = x.AreaID,
                                        UrgencyLevel = x.UrgencyLevel,
                                        TimeConstraint = x.TimeConstraint,
                                        ResourceAffectedAreas = x.ResourceAffectedAreas.Select(r => new DTOs.AffectedAreas.ResourceAffectedAreasDTOs
                                        {
                                            ResourceID = r.ResourceID,
                                            Quantity = r.Quantity,
                                            Name = r.MasterResource.Name
                                        }).ToList()
                                    }).ToList();
            }
            return result;
        }
    }
}