namespace Hyperar.HUM.Infrastructure.Database.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class SeniorPlayerAvatarLayer : EntityConfigurationBase<Domain.SeniorPlayerAvatarLayer>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.SeniorPlayerAvatarLayer> builder)
        {
            builder.Property(p => p.ImageUrl)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.Index)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.XCoordinate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.YCoordinate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.Type)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.SeniorPlayerAvatarLayer> builder)
        {
            builder.HasOne(m => m.SeniorPlayer)
                .WithMany(m => m.AvatarLayers)
                .HasConstraintName("FK_SeniorPlayerAvatarLayer_SeniorPlayer")
                .HasForeignKey(m => m.SeniorPlayerHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}