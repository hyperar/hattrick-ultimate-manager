namespace Hyperar.HUM.Infrastructure.Database
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain;
    using Hyperar.HUM.Domain.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class HattrickRepository<TEntity> : RepositoryBase<TEntity>, IHattrickRepository<TEntity> where TEntity : HattrickEntityBase
    {
        public HattrickRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task DeleteAsync(long hattickId)
        {
            var entity = await this.GetByIdAsync(hattickId);

            ArgumentNullException.ThrowIfNull(entity);

            await base.DeleteAsync(entity);
        }

        public async Task DeleteRangeAsync(ICollection<long> hattrickIds)
        {
            var entities = await this.Query(x => hattrickIds.Contains(x.HattrickId))
                .ToListAsync();

            await base.DeleteAsync(entities);
        }

        public async Task<TEntity?> GetByIdAsync(long hattickId)
        {
            return await this.EntityCollection.Where(x => x.HattrickId == hattickId)
                .SingleOrDefaultAsync();
        }
    }
}