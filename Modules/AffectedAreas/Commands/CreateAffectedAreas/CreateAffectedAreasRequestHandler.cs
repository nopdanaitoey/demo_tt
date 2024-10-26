using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Entities;
using demo_tt.Entities.Constants;
using demo_tt.Repositories.IRepositories;
using Mapster;

namespace demo_tt.Modules.AffectedAreas.Commands.CreateAffectedAreas
{
    public class CreateAffectedAreasRequestHandler : IRequestHandler<CreateAffectedAreasRequest, CreateAffectedAreasResponse>
    {
        private readonly IAffectedAreasRepository _affectedAreasRepository;
        private readonly IMasterResourceRepository _masterResourceRepository;
        private readonly ICacheRepository _cacheRepository;
        public CreateAffectedAreasRequestHandler(IAffectedAreasRepository affectedAreasRepository,
            IMasterResourceRepository masterResourceRepository,
            ICacheRepository cacheRepository)
        {
            _affectedAreasRepository = affectedAreasRepository;
            _masterResourceRepository = masterResourceRepository;
            _cacheRepository = cacheRepository;
        }

        public async Task<CreateAffectedAreasResponse> Handle(CreateAffectedAreasRequest request, CancellationToken cancellationToken)
        {
            CreateAffectedAreasResponse result = new CreateAffectedAreasResponse();

            if (request.TimeConstraint < 0) throw new Exception("Time Constraint cannot be less than 0");
            if (request.UrgencyLevel < 0) throw new Exception("Urgency Level cannot be less than 0");
            if (request.UrgencyLevel > ConstantsEntity.MAX_URGENCY_LEVEL) throw new Exception($"Urgency Level cannot be greater than {ConstantsEntity.MAX_URGENCY_LEVEL}");
            var existArea = await _affectedAreasRepository.GetByAreaIDAsync(request.AreaID);
            if (existArea is not null) throw new Exception("Area already exist");

            if (request.RequiredResources is null || request.RequiredResources.Count == 0) throw new Exception("Required Resources not found");

            var existMasterResource = await _masterResourceRepository.GetListMasterResource(ConstantsEntity.ACTIVE);
            if (request.RequiredResources.Any(x => !existMasterResource.Any(y => y.Id == x.ResourceID))) throw new Exception("Resource not found");

            Entities.AffectedAreas newAffectedAreas = request.Adapt<Entities.AffectedAreas>();
            newAffectedAreas.Id = Guid.NewGuid();
            newAffectedAreas.ResourceAffectedAreas = new List<Entities.ResourceAffectedAreas>();
            foreach (var item in request.RequiredResources)
            {
                newAffectedAreas.ResourceAffectedAreas.Add(new Entities.ResourceAffectedAreas
                {
                    AreaID = newAffectedAreas.Id,
                    ResourceID = item.ResourceID,
                    Quantity = item.Quantity
                });
            }

            var createdAffectedArea = await _affectedAreasRepository.Add(newAffectedAreas, cancellationToken);
            result.IsSuccess = createdAffectedArea != null;
            result.Message = result.IsSuccess is true ? "Success" : "Fail";

            if (result.IsSuccess is true && createdAffectedArea is not null)
            {
                var existCache = await _cacheRepository.GetCacheData<List<Entities.AffectedAreas>>(ConstantsEntity.REDIS_AFFECTED_AREAS);
                if (existCache is null) existCache = new List<Entities.AffectedAreas>();
                existCache.Add(createdAffectedArea);

            }

            return result;
        }
    }
}