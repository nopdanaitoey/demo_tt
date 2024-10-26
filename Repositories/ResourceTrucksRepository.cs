using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Database;
using demo_tt.Entities;
using demo_tt.Repositories.IRepositories;

namespace demo_tt.Repositories
{
    public class ResourceTrucksRepository : GenericRepository<ResourceTrucks>, IResourceTrucksRepository
    {
        public ResourceTrucksRepository(DataBaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}