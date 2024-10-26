using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Entities;

namespace demo_tt.Repositories.IRepositories
{
    public interface IMasterResourceRepository : IGenericRepository<MasterResource>
    {
        Task<List<MasterResource>> GetListMasterResource(bool? isActive = null);
    }
}