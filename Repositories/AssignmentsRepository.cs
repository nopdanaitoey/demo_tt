using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Database;
using demo_tt.Entities;
using demo_tt.Entities.Constants;
using demo_tt.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace demo_tt.Repositories
{
    public class AssignmentsRepository : GenericRepository<Assignments>, IAssignmentsRepository
    {
        private readonly DataBaseContext _databaseContext;
        public AssignmentsRepository(DataBaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<bool> DeleteAssignmentByDateAsync(DateTime? date = null)
        {
            DateTime dateRequest = date is null ? DateTime.Now : date.Value;
            var result = await _databaseContext.Assignments.Where(x => x.CreatedTime.Date == dateRequest.Date
                                                            && x.IsDeleted == ConstantsEntity.NOT_DELETED
                                                            && x.IsActive == ConstantsEntity.ACTIVE)
                                                            .ExecuteUpdateAsync(u => u.SetProperty(d => d.IsDeleted, ConstantsEntity.DELETED));
            return result > 0;
        }

        public async Task<List<Assignments>> GetListAssignmentsAsync(bool? isDelete = null)
        {
            var query = _databaseContext.Assignments.Where(x => x.IsActive == ConstantsEntity.ACTIVE).AsQueryable();

            if (isDelete is not null) query = query.Where(x => x.IsDeleted == isDelete);

            var result = await query.Include(x => x.AffectedAreas)
                                    .ThenInclude(x => x.ResourceAffectedAreas)
                                    .ThenInclude(x => x.MasterResource)
                                    .Include(x => x.Trucks)
                                    .OrderByDescending(x => x.AffectedAreas.UrgencyLevel)
                                    .ThenByDescending(x => x.CreatedTime)
                                    .ToListAsync();


            return result;
        }
    }
}