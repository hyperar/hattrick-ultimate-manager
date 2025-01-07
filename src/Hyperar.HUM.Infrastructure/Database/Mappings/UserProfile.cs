namespace Hyperar.HUM.Infrastructure.Database.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class UserProfile : EntityConfigurationBase<Domain.UserProfile>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.UserProfile> builder)
        {
            builder.Property(x => x.LastDownloadDate)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime);

            builder.Property(x => x.SelectedTeamHattrickId)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.BigInt);
        }

        public override void MapTable(EntityTypeBuilder<Domain.UserProfile> builder)
        {
            builder.ToTable(Constants.TableName.UserProfile);
        }
    }
}