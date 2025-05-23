namespace Hyperar.HUM.Infrastructure.Database.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class SeniorTeam : HattrickEntityConfigurationBase<Domain.SeniorTeam>
    {
        public override void MapProperties(EntityTypeBuilder<Domain.SeniorTeam> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.ShortName)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(p => p.FoundedOn)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.DateTime)
                .IsRequired();

            builder.Property(p => p.TeamIndex)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.IsPlayingCup)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.GlobalPowerRating)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.LeaguePowerRating)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.RegionPowerRating)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.PowerRating)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.LeagueRank)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.UndefeatedStreak)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.WinningStreak)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Int)
                .IsRequired();

            builder.Property(p => p.CanBookMidweekFriendly)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.CanBookWeekendFriendly)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.Bit)
                .IsRequired();

            builder.Property(p => p.SeriesHattrickId)
                            .HasColumnOrder(
                                this.GetColumnOrder())
                            .HasColumnType(Constants.ColumnType.BigInt)
                            .IsRequired();

            builder.Property(p => p.SeriesName)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.NVarChar)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.LogoBytes)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.VarBinary);

            builder.Property(p => p.HomeMatchKitBytes)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.VarBinary)
                .IsRequired();

            builder.Property(p => p.AwayMatchKitBytes)
                .HasColumnOrder(
                    this.GetColumnOrder())
                .HasColumnType(Constants.ColumnType.VarBinary)
                .IsRequired();
        }

        public override void MapRelationships(EntityTypeBuilder<Domain.SeniorTeam> builder)
        {
            builder.HasOne(m => m.League)
                .WithMany(m => m.SeniorTeams)
                .HasConstraintName("FK_SeniorTeam_League")
                .HasForeignKey(m => m.LeagueHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Region)
                .WithMany(m => m.SeniorTeams)
                .HasConstraintName("FK_SeniorTeam_Region")
                .HasForeignKey(m => m.RegionHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(m => m.Manager)
                .WithMany(m => m.SeniorTeams)
                .HasConstraintName("FK_SeniorTeam_Manager")
                .HasForeignKey(m => m.ManagerHattrickId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override void MapTable(EntityTypeBuilder<Domain.SeniorTeam> builder)
        {
            builder.ToTable(Constants.TableName.SeniorTeam);
        }
    }
}