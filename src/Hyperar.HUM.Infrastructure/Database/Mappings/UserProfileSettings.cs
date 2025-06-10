namespace Hyperar.HUM.Infrastructure.Database.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UserProfileSettings : EntityConfigurationBase<Domain.UserProfileSettings>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.UserProfileSettings> builder)
        {
            builder.Property(p => p.UseFramelessAvatars)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .HasMaxLength(256)
                .IsRequired();
        }

        public override void MapTable(EntityTypeBuilder<Domain.UserProfileSettings> builder)
        {
            builder.HasOne(x => x.UserProfile)
                .WithOne(x => x.UserProfileSettings)
                .HasConstraintName("FK_UserProfileSettings_UserProfile")
                .HasForeignKey<Domain.UserProfileSettings>(x => x.UserProfileId);
        }
    }
}