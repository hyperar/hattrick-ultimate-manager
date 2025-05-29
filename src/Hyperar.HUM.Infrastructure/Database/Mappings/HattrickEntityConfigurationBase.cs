namespace Hyperar.HUM.Infrastructure.Database.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal abstract class HattrickEntityConfigurationBase<TEntity> : EntityMappingBase<TEntity> where TEntity : class, Domain.Interfaces.IHattrickEntity
    {
        public sealed override void MapBaseProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.HattrickId);

            builder.Property(x => x.HattrickId)
                .HasColumnType(Constants.ColumnType.BigInt)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .ValueGeneratedNever()
                .IsRequired();
        }
    }
}