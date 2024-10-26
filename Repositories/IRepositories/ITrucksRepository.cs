using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Entities;

namespace demo_tt.Repositories.IRepositories
{
    public interface ITrucksRepository : IGenericRepository<Trucks>
    {
        Task<Trucks> GetByTruckIDAsync(string truckID);

        Task<List<Trucks>> GetTrucksNotAssignedAsync();
    }
}