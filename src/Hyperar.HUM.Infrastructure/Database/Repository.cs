namespace Hyperar.HUM.Infrastructure.Database
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain;
    using Hyperar.HUM.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class Repository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity> where TEntity : EntityBase
    {
        public Repository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task DeleteAsync(Guid id)
        {
            TEntity? entity = await this.GetByIdAsync(id);

            ArgumentNullException.ThrowIfNull(entity);

            entity.DeletedOn = DateTime.Now;

            await base.UpdateAsync(entity);

            await base.DeleteAsync(entity);
        }

        public async Task DeleteRangeAsync(ICollection<Guid> ids)
        {
            List<TEntity> entities = await this.Query(x => ids.Contains(x.Id))
                .ToListAsync();

            entities.ForEach(x =>
            {
                x.DeletedOn = DateTime.Now;
            });

            await base.UpdateAsync(entities);

            await base.DeleteAsync(entities);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await this.EntityCollection.Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }
    }
}