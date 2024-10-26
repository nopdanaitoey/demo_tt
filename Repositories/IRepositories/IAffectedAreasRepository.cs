using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Entities;

namespace demo_tt.Repositories.IRepositories
{
    public interface IAffectedAreasRepository : IGenericRepository<AffectedAreas>
    {
        Task<AffectedAreas> GetByAreaIDAsync(string areaID);
        Task<List<AffectedAreas>> GetAffectedAreasNotAssignedAsync();
    }
}