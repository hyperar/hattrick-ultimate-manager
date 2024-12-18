namespace Hyperar.HUM.Infrastructure.Database.Mappings
{
    using Hyperar.HUM.Infrastructure.Database.Interfaces;
    using Hyperar.HUM.Infrastructure.Database.ValueGenerators;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal abstract class AuditableEntityConfigurationBase<TEntity> : EntityConfigurationBase<TEntity>, IAuditableEntityMapping<TEntity> where TEntity : class, Domain.Interfaces.IEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            this.MapTable(builder);
            this.MapBaseProperties(builder);
            this.MapProperties(builder);
            this.MapAuditProperties(builder);
            this.MapRelationships(builder);
        }

        public void MapAuditProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(p => p.CreatedOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.UpdatedOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime);

            builder.Property(p => p.DeletedOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime);
        }

        protected override sealed void MapBaseProperties(EntityTypeBuilder<TEntity> builder)
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