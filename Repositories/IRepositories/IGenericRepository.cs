using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_tt.Repositories.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(Guid id, CancellationToken? cancellationToken = default);
        Task<List<T>> GetAll(CancellationToken? cancellationToken = default);

        Task<T> Add(T entity, CancellationToken? cancellationToken = default);

        Task Update(T entity, CancellationToken? cancellationToken = default);
        Task Delete(T entity, CancellationToken? cancellationToken = default);
        Task DeleteRage(List<T> entity, CancellationToken? cancellationToken = default);
        Task<List<T>> AddRage(List<T> entity, CancellationToken? cancellationToken = default);

    }
}