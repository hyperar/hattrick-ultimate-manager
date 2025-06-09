namespace Hyperar.HUM.Infrastructure.Database.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ManagerAvatarLayer : EntityConfigurationBase<Domain.ManagerAvatarLayer>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.ManagerAvatarLayer> builder)
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

        public override void MapTable(EntityTypeBuilder<Domain.ManagerAvatarLayer> builder)
        {
            builder.HasOne(m => m.Manager)
                .WithMany(m => m.AvatarLayers)
                .HasConstraintName("FK_ManagerAvatarLayer_Manager")
                .HasForeignKey(m => m.ManagerHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}