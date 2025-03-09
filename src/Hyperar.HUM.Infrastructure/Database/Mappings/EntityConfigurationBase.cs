namespace Hyperar.HUM.Infrastructure.Database.Mappings
{
    using Hyperar.HUM.Infrastructure.Database.Interfaces;
    using Hyperar.HUM.Infrastructure.Database.ValueGenerators;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal abstract class EntityConfigurationBase<TEntity> : EntityMappingBase<TEntity>, IEntityMapping<TEntity> where TEntity : class, Domain.Interfaces.IEntity
    {
        public override sealed void MapBaseProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.Id)
                .HasColumnType(Constants.ColumnType.UniqueIdentifier)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasValueGenerator<UniqueIdentifier>()
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}