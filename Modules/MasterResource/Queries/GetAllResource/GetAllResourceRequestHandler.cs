
using demo_tt.Entities.Constants;
using demo_tt.Repositories.IRepositories;
using Mapster;


namespace demo_tt.Modules.MasterResource.Queries.GetAllResource
{
    public class GetAllResourceRequestHandler : IRequestHandler<GetAllResourceRequest, List<GetAllResourceResponse>>
    {
        private readonly IMasterResourceRepository _masterResourceRepository;
        private readonly ICacheRepository _cacheRepository;
        public GetAllResourceRequestHandler(IMasterResourceRepository masterResourceRepository,
        ICacheRepository cacheRepository)
        {
            _masterResourceRepository = masterResourceRepository;
            _cacheRepository = cacheRepository;
        }

        public async Task<List<GetAllResourceResponse>> Handle(GetAllResourceRequest request, CancellationToken cancellationToken)
        {
            List<GetAllResourceResponse> result = new List<GetAllResourceResponse>();
            var existCacheData = await _cacheRepository.GetCacheData<List<GetAllResourceResponse>>(ConstantsEntity.REDIS_MASTER_RESOURCE);
            if (existCacheData is not null && existCacheData.Count > 0)
            {
                result = existCacheData;
            }
            else
            {
                var data = await _masterResourceRepository.GetListMasterResource(ConstantsEntity.ACTIVE);
                if (data is not null && data.Count > 0)
                {
                    result = data.Adapt<List<GetAllResourceResponse>>();
                    await _cacheRepository.SetCacheData<List<GetAllResourceResponse>>(ConstantsEntity.REDIS_MASTER_RESOURCE, result, DateTimeOffset.Now.AddMinutes(ConstantsEntity.MAX_TIME_REDIS_MIN));
                }

            }

            return result;
        }
    }
}