using FuraFila.Domain.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FuraFila.Domain.Repositories
{
    public interface IGenericRepository<T>
        where T : class, IEntity<string>, new()
    {
        Task<T> Create(T entity);

        Task<bool> Delete(string id);

        IQueryable<T> GetAll();

        Task<T> GetById(string id);

        Task<T> GetById<TProperty>(string id, Expression<Func<T, TProperty>> navigationPropertyPath);

        Task<T> Update(string id, T entity);
    }
}
