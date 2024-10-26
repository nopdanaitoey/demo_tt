
using demo_tt.Modules.AffectedAreas.Commands.CreateAffectedAreas;
using demo_tt.Modules.AffectedAreas.Queries.GetListAreas;
using demo_tt.Responses;
using Microsoft.AspNetCore.Mvc;

namespace demo_tt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AreasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<BaseResponse> Post(CreateAffectedAreasRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            if (!result.IsSuccess) return new BaseResponse().Fail(result.Message);
            return new BaseResponse().Success(result.Message);
        }
        [HttpGet]
        public async Task<BaseResponse> Get()
        {
            var result = await _mediator.Send(new GetListAreasRequest());

            return new BaseResponse().Success(result.AffectedAreas);
        }
    }
}