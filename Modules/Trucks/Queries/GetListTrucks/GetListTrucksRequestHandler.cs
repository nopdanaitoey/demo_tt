using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.DTOs.Trucks;
using demo_tt.Entities.Constants;
using demo_tt.Repositories.IRepositories;
using Mapster;

namespace demo_tt.Modules.Trucks.Queries.GetListTrucks
{
    public class GetListTrucksRequestHandler : IRequestHandler<GetListTrucksRequest, GetListTrucksResponse>
    {

        private readonly ICacheRepository _cacheRepository;
        public GetListTrucksRequestHandler(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<GetListTrucksResponse> Handle(GetListTrucksRequest request, CancellationToken cancellationToken)
        {
            GetListTrucksResponse result = new GetListTrucksResponse();
            var existCacheTrucks = await _cacheRepository.GetCacheData<List<Entities.Trucks>>(ConstantsEntity.REDIS_TRUCKS);
            if (existCacheTrucks is not null && existCacheTrucks.Count > 0)
            {
                var existMasterResource = await _cacheRepository.GetCacheData<List<Entities.MasterResource>>(ConstantsEntity.REDIS_MASTER_RESOURCE);
                result.Trucks = existCacheTrucks.Adapt<List<TrucksDTOs>>();
                foreach (var item in result.Trucks)
                {

                    foreach (var resource in item.ResourceTrucks)
                    {
                        var masterResource = existMasterResource.Where(x => x.Id == resource.ResourceID).FirstOrDefault();
                        resource.Name = masterResource.Name;
                    }
                }

            }
            return result;
        }
    }
}