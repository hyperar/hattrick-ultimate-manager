namespace Hyperar.HUM.Infrastructure.Database.Interfaces
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal interface IEntityMapping<TEntity> where TEntity : class
    {
        void MapProperties(EntityTypeBuilder<TEntity> builder);

        void MapRelationships(EntityTypeBuilder<TEntity> builder);

        void MapTable(EntityTypeBuilder<TEntity> builder);
    }
}