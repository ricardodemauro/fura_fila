using FuraFila.Domain.Infrastructure.Entities;
using FuraFila.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FuraFila.Repository.EF.Repositories
{
    public class EFGenericRepository<T> : IGenericRepository<T>
        where T : class, IEntity<string>, new()
    {
        private readonly DbContext _dbContext;

        public EFGenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<T> Create(T entity)
        {
            var et = await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return et.Entity;
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await GetById(id);

            _dbContext.Set<T>().Remove(entity);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public async Task<T> GetById(string id)
        {
            return await _dbContext.Set<T>()
                            .AsNoTracking()
                            .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> GetById<TProperty>(string id, Expression<Func<T, TProperty>> navigationPropertyPath)
        {
            return await _dbContext.Set<T>()
                            .Include(navigationPropertyPath)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<T> Update(string id, T entity)
        {
            var et = _dbContext.Set<T>().Update(entity);

            await _dbContext.SaveChangesAsync();

            return et.Entity;
        }
    }
}
