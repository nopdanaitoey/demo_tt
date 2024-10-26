using demo_tt.Modules.MasterResource.Queries.GetAllResource;
using Microsoft.AspNetCore.Mvc;

namespace demo_tt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ResourceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]

        public async Task<IActionResult> Get()
        {
            
            return Ok(await _mediator.Send(new GetAllResourceRequest()));
        }
    }
}