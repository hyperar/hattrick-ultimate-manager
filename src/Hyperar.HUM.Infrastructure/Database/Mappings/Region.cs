namespace Hyperar.HUM.Infrastructure.Database.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class Region : HattrickEntityConfigurationBase<Domain.Region>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.Region> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.Region> builder)
        {
            builder.HasOne(m => m.Country)
                .WithMany(m => m.Regions)
                .HasConstraintName("FK_Region_Country")
                .HasForeignKey(m => m.CountryHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.Region> builder)
        {
            builder.ToTable(Constants.TableName.Region);
        }
    }
}