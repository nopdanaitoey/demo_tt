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
    public class TrucksRepository : GenericRepository<Trucks>, ITrucksRepository
    {
        private readonly DataBaseContext _databaseContext;
        public TrucksRepository(DataBaseContext databaseContext) : base(databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Trucks> GetByTruckIDAsync(string truckID)
        {
            var existTruck = await _databaseContext.Trucks.Where(x => x.TruckID == truckID).FirstOrDefaultAsync();
            return existTruck;
        }

        public async Task<List<Trucks>> GetTrucksNotAssignedAsync()
        {
            var result = await _databaseContext.Trucks
                                .Include(x => x.ResourceTrucks)
                                .Include(x => x.Assignments)
                                .Include(x => x.TravelTimeToAreas)
                                .Where(x => x.IsActive == ConstantsEntity.ACTIVE
                                    && x.ResourceTrucks.Where(r => r.IsActive == ConstantsEntity.ACTIVE && r.AvailableQuantity < 0).Count() == 0
                                    && x.Assignments.Where(a => a.IsActive == ConstantsEntity.ACTIVE
                                    && a.IsDeleted == ConstantsEntity.NOT_DELETED).Count() == 0)
                                    .ToListAsync();
            return result;
        }
    }
}