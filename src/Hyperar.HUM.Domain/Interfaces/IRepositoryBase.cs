namespace Hyperar.HUM.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task DeleteAsync(TEntity entity);

        Task DeleteAsync(ICollection<TEntity> entities);

        Task InsertAsync(TEntity entity);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null);

        Task UpdateAsync(TEntity entity);

        Task UpdateAsync(ICollection<TEntity> entities);
    }
}