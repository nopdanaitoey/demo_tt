
using demo_tt.Modules.Trucks.Commands.CreateTruck;
using demo_tt.Modules.Trucks.Queries.GetListTrucks;
using demo_tt.Responses;
using Microsoft.AspNetCore.Mvc;

namespace demo_tt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrucksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TrucksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<BaseResponse> Post(CreateTruckRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.IsSuccess is false) return new BaseResponse().Fail(result.Message);
            return new BaseResponse().Success(result.Message);
        }
        [HttpGet]
        public async Task<BaseResponse> Get()
        {
            var result = await _mediator.Send(new GetListTrucksRequest());

            return new BaseResponse().Success(result.Trucks);
        }
    }
}