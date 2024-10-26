using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Entities.Constants;
using demo_tt.Repositories.IRepositories;

namespace demo_tt.Modules.Assignments.Commands.DeleteAssignment
{
    public class DeleteAssignmentRequestHandler : IRequestHandler<DeleteAssignmentRequest, DeleteAssignmentResponse>
    {
        private readonly IAssignmentsRepository _assignmentRepository;
        private readonly ICacheRepository _cacheRepository;
        public DeleteAssignmentRequestHandler(IAssignmentsRepository assignmentRepository,
            ICacheRepository cacheRepository)
        {
            _assignmentRepository = assignmentRepository;
            _cacheRepository = cacheRepository;
        }

        public async Task<DeleteAssignmentResponse> Handle(DeleteAssignmentRequest request, CancellationToken cancellationToken)
        {
            DeleteAssignmentResponse result = new DeleteAssignmentResponse();
            await _assignmentRepository.DeleteAssignmentByDateAsync();

            await _cacheRepository.RemoveData(ConstantsEntity.REDIS_ASSIGNMENTS);

            result.IsSuccess = true;
            result.Message = result.IsSuccess is true ? "Success" : "Fail";
            return result;
        }
    }
}