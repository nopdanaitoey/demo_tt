using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Database;
using demo_tt.Entities;
using demo_tt.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace demo_tt.Repositories
{
    public class MasterResourceRepository : GenericRepository<MasterResource>, IMasterResourceRepository
    {
        private readonly DataBaseContext _databaseContext;
        public MasterResourceRepository(DataBaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<MasterResource>> GetListMasterResource(bool? isActive = null)
        {
            var query = _databaseContext.MasterResources.AsQueryable();
            if (isActive is not null) query = query.Where(x => x.IsActive == isActive);
            var result = await query.ToListAsync();
            return result;
        }
    }
}