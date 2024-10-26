using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Database;
using demo_tt.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace demo_tt.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataBaseContext _databaseContext;
        public GenericRepository(DataBaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<T> Add(T entity, CancellationToken? cancellationToken = default)
        {
            await _databaseContext.AddAsync(entity);
            await _databaseContext.SaveChangesAsync(cancellationToken ?? default);
            return entity;
        }

        public async Task<List<T>> AddRage(List<T> entity, CancellationToken? cancellationToken = default)
        {
            await _databaseContext.AddRangeAsync(entity);
            await _databaseContext.SaveChangesAsync(cancellationToken ?? default);
            return entity;
        }

        public async Task Delete(T entity, CancellationToken? cancellationToken = default)
        {
            _databaseContext.Set<T>().Remove(entity);
            await _databaseContext.SaveChangesAsync(cancellationToken ?? default);
        }

        public async Task DeleteRage(List<T> entity, CancellationToken? cancellationToken = default)
        {
            _databaseContext.Set<T>().RemoveRange(entity);
            await _databaseContext.SaveChangesAsync(cancellationToken ?? default);
        }

        public async Task<T> Get(Guid id, CancellationToken? cancellationToken = default)
        {
            return await _databaseContext.Set<T>().FindAsync(id, cancellationToken);
        }

        public async Task<List<T>> GetAll(CancellationToken? cancellationToken = default)
        {
            return await _databaseContext.Set<T>().ToListAsync(cancellationToken ?? default);
        }



        public async Task Update(T entity, CancellationToken? cancellationToken = default)
        {
            _databaseContext.Entry(entity).State = EntityState.Modified;
            await _databaseContext.SaveChangesAsync(cancellationToken ?? default);
        }
    }
}