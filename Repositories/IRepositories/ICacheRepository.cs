using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_tt.Repositories.IRepositories
{
    public interface ICacheRepository
    {
        Task<T> GetCacheData<T>(string key);
        Task<object> RemoveData(string key);
        Task<bool> SetCacheData<T>(string key, T value, DateTimeOffset expiration);
    }
}