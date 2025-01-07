namespace Hyperar.HUM.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        Task DeleteAsync(Guid id);

        Task DeleteRangeAsync(ICollection<Guid> ids);

        Task<TEntity?> GetByIdAsync(Guid id);
    }
}