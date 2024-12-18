namespace Hyperar.HUM.Infrastructure.Database.Interfaces
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal interface IAuditableEntityMapping<TEntity> where TEntity : class
    {
        void MapAuditProperties(EntityTypeBuilder<TEntity> builder);
    }
}