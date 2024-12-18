namespace Hyperar.HUM.Infrastructure.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected RepositoryBase(IDatabaseContext databaseContext)
        {
            this.EntityCollection = databaseContext.CreateSet<TEntity>();
        }

        protected DbSet<TEntity> EntityCollection { get; }

        public Task DeleteAsync(TEntity entity)
        {
            this.EntityCollection.Remove(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(ICollection<TEntity> entities)
        {
            this.EntityCollection.RemoveRange(entities);

            return Task.CompletedTask;
        }

        public async Task InsertAsync(TEntity entity)
        {
            await this.EntityCollection.AddAsync(entity);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null)
        {
            predicate ??= x => true;

            return this.EntityCollection.Where(predicate);
        }

        public Task UpdateAsync(TEntity entity)
        {
            this.EntityCollection.Update(entity);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(ICollection<TEntity> entities)
        {
            this.EntityCollection.UpdateRange(entities);

            return Task.CompletedTask;
        }
    }
}