namespace Hyperar.HUM.Infrastructure.Database.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class OAuthToken : EntityConfigurationBase<Domain.OAuthToken>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.OAuthToken> builder)
        {
            builder.Property(x => x.Token)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.Secret)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(x => x.Scope)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(x => x.CreatedOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(x => x.ExpiresOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.OAuthToken> builder)
        {
            builder.HasOne(x => x.UserProfile)
                .WithOne(x => x.OAuthToken)
                .HasConstraintName("FK_OAuthToken_UserProfile")
                .HasForeignKey<Domain.OAuthToken>(x => x.UserProfileId);
        }

        public override void MapTable(EntityTypeBuilder<Domain.OAuthToken> builder)
        {
            builder.ToTable(Constants.TableName.OAuthToken);
        }
    }
}