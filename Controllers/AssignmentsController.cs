using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Modules.Assignments.Commands.CreateAssignment;
using demo_tt.Modules.Assignments.Commands.DeleteAssignment;
using demo_tt.Modules.Assignments.Queries.GetListAssignment;
using demo_tt.Responses;
using Microsoft.AspNetCore.Mvc;

namespace demo_tt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AssignmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]

        public async Task<BaseResponse> Get()
        {
            var result = await _mediator.Send(new GetListAssignmentRequest());

            return new BaseResponse().Success(result.Assignments);
        }

        [HttpPost]
        public async Task<BaseResponse> Post()
        {
            var result = await _mediator.Send(new CreateAssignmentRequest());
            if (!result.IsSuccess) return new BaseResponse().Fail(result.Message);
            return new BaseResponse().Success(result.Assignments, result.Message);
        }
        [HttpDelete]
        public async Task<BaseResponse> Delete()
        {
            var result = await _mediator.Send(new DeleteAssignmentRequest());
            if (!result.IsSuccess) return new BaseResponse().Fail(result.Message);
            return new BaseResponse().Success(result.Message);
        }
    }
}