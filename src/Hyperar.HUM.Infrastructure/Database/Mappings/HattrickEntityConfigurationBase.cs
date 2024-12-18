namespace Hyperar.HUM.Infrastructure.Database.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal abstract class HattrickEntityConfigurationBase<TEntity> : EntityConfigurationBase<TEntity> where TEntity : class, Domain.Interfaces.IHattrickEntity
    {
        protected override sealed void MapBaseProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.HattrickId)
                .HasColumnType(Constants.ColumnType.BigInt)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .IsRequired();
        }
    }
}