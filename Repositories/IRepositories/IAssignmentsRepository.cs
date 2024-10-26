using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Entities;

namespace demo_tt.Repositories.IRepositories
{
    public interface IAssignmentsRepository : IGenericRepository<Assignments>
    {
        Task<List<Assignments>> GetListAssignmentsAsync(bool? isDelete = null);
        Task<bool> DeleteAssignmentByDateAsync(DateTime? date = null);
    }
}