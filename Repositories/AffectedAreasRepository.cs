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
    public class AffectedAreasRepository : GenericRepository<AffectedAreas>, IAffectedAreasRepository
    {
        private readonly DataBaseContext _databaseContext;
        public AffectedAreasRepository(DataBaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<AffectedAreas>> GetAffectedAreasNotAssignedAsync()
        {
            var result = await _databaseContext.AffectedAreas
                                .Include(x => x.TravelTimeToAreas)
                                .Include(x => x.ResourceAffectedAreas)
                                .ThenInclude(x => x.MasterResource)
                                .Include(x => x.Assignments)
                                .Where(x => x.IsActive == ConstantsEntity.ACTIVE
                                && x.Assignments.Where(a => a.IsActive == ConstantsEntity.ACTIVE
                                && a.IsDeleted == ConstantsEntity.NOT_DELETED).Count() == 0
                                )
                                .OrderByDescending(x => x.UrgencyLevel)
                                .ToListAsync();
            return result;
        }

        public async Task<AffectedAreas> GetByAreaIDAsync(string areaID)
        {
            var result = await _databaseContext.AffectedAreas.Where(x => x.AreaID == areaID).FirstOrDefaultAsync();
            return result;



        }
    }
}