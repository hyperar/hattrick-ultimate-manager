namespace Hyperar.HUM.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IHattrickRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : HattrickEntityBase
    {
        Task DeleteAsync(long hattrickId);

        Task DeleteRangeAsync(ICollection<long> hattrickIds);

        Task<TEntity?> GetByIdAsync(long hattickId);
    }
}