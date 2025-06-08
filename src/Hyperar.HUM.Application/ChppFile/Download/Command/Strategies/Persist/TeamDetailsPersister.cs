namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Models.Chpp.TeamDetails;
    using Microsoft.EntityFrameworkCore;

    public class TeamDetailsPersister : IFilePersisterStrategy
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.League> leagueRepository;

        private readonly IHattrickRepository<Domain.Manager> managerRepository;

        private readonly IHattrickRepository<Domain.Region> regionRepository;

        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public TeamDetailsPersister(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.League> leagueRepository,
            IHattrickRepository<Domain.Manager> managerRepository,
            IHattrickRepository<Domain.Region> regionRepository,
            IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository)
        {
            this.databaseContext = databaseContext;
            this.leagueRepository = leagueRepository;
            this.managerRepository = managerRepository;
            this.regionRepository = regionRepository;
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public async Task PersistFileAsync(FileDownloadTaskBase fileDownloadTask, CancellationToken cancellationToken)
        {
            var xmlFileDownloadTask = fileDownloadTask as XmlFileDownloadTask;

            ArgumentNullException.ThrowIfNull(xmlFileDownloadTask);

            var teamDetails = xmlFileDownloadTask.Entity as HattrickData;

            ArgumentNullException.ThrowIfNull(teamDetails);

            var manager = await this.managerRepository.GetByIdAsync(teamDetails.User.UserId);

            ArgumentNullException.ThrowIfNull(manager);

            var formerTeamIds = await this.seniorTeamRepository.Query(x => x.ManagerHattrickId == manager.HattrickId)
                .Select(x => x.HattrickId)
                .Except(
                    teamDetails.Teams.Select(x => x.TeamId))
                .ToArrayAsync(cancellationToken);

            if (formerTeamIds.Length > 0)
            {
                await this.seniorTeamRepository.DeleteRangeAsync(formerTeamIds);
            }

            for (var i = 0; i < teamDetails.Teams.Length; i++)
            {
                await this.PersistTeamNodeAsync(
                    teamDetails.Teams[i],
                    i,
                    manager,
                    cancellationToken);
            }
        }

        private async Task PersistTeamNodeAsync(Team teamNode, int teamIndex, Domain.Manager manager, CancellationToken cancellationToken)
        {
            var seniorTeam = await this.seniorTeamRepository.GetByIdAsync(teamNode.TeamId);

            var region = await this.regionRepository.GetByIdAsync(
                    teamNode.Region.Id);

            ArgumentNullException.ThrowIfNull(region);

            if (seniorTeam == null)
            {
                var league = await this.leagueRepository.GetByIdAsync(
                        teamNode.League.Id);

                ArgumentNullException.ThrowIfNull(league);

                seniorTeam = new Domain.SeniorTeam
                {
                    HattrickId = teamNode.TeamId,
                    Name = teamNode.TeamName,
                    ShortName = teamNode.ShortTeamName,
                    FoundedOn = teamNode.FoundedDate,
                    TeamIndex = teamIndex,
                    IsPlayingCup = teamNode.Cup?.StillInCup ?? false,
                    GlobalPowerRating = teamNode.PowerRating.GlobalRanking,
                    LeaguePowerRating = teamNode.PowerRating.LeagueRanking,
                    RegionPowerRating = teamNode.PowerRating.RegionRanking,
                    PowerRating = teamNode.PowerRating.Value,
                    LeagueRank = teamNode.TeamRank ?? 0,
                    UndefeatedStreak = teamNode.NumberOfUndefeated ?? 0,
                    WinningStreak = teamNode.NumberOfVictories ?? 0,
                    CanBookMidweekFriendly = teamNode.PossibleToChallengeMidweek,
                    CanBookWeekendFriendly = teamNode.PossibleToChallengeWeekend,
                    SeriesHattrickId = teamNode.LeagueLevelUnit.LeagueLevelUnitId,
                    SeriesName = teamNode.LeagueLevelUnit.LeagueLevelUnitName,
                    LogoBytes = string.IsNullOrEmpty(teamNode.LogoUrl)
                        ? null
                        : await ImageHelpers.ReadFileFromCacheAsync(
                            teamNode.LogoUrl,
                            cancellationToken),
                    HomeMatchKitBytes = await ImageHelpers.ReadFileFromCacheAsync(
                            teamNode.DressUri,
                            cancellationToken),
                    AwayMatchKitBytes = await ImageHelpers.ReadFileFromCacheAsync(
                            teamNode.AlternateDressUri,
                            cancellationToken),
                    League = league,
                    Manager = manager,
                    Region = region
                };

                await this.seniorTeamRepository.InsertAsync(
                    seniorTeam);
            }
            else
            {
                seniorTeam.Name = teamNode.TeamName;
                seniorTeam.ShortName = teamNode.ShortTeamName;
                seniorTeam.IsPlayingCup = teamNode.Cup?.StillInCup ?? false;
                seniorTeam.GlobalPowerRating = teamNode.PowerRating.GlobalRanking;
                seniorTeam.LeaguePowerRating = teamNode.PowerRating.LeagueRanking;
                seniorTeam.RegionPowerRating = teamNode.PowerRating.RegionRanking;
                seniorTeam.PowerRating = teamNode.PowerRating.Value;
                seniorTeam.LeagueRank = teamNode.TeamRank ?? 0;
                seniorTeam.UndefeatedStreak = teamNode.NumberOfUndefeated ?? 0;
                seniorTeam.WinningStreak = teamNode.NumberOfVictories ?? 0;
                seniorTeam.CanBookMidweekFriendly = teamNode.PossibleToChallengeMidweek;
                seniorTeam.CanBookWeekendFriendly = teamNode.PossibleToChallengeWeekend;
                seniorTeam.LogoBytes = string.IsNullOrEmpty(teamNode.LogoUrl)
                    ? null
                    : await ImageHelpers.ReadFileFromCacheAsync(
                        teamNode.LogoUrl,
                        cancellationToken);
                seniorTeam.HomeMatchKitBytes = await ImageHelpers.ReadFileFromCacheAsync(
                    teamNode.DressUri,
                    cancellationToken);
                seniorTeam.AwayMatchKitBytes = await ImageHelpers.ReadFileFromCacheAsync(
                    teamNode.AlternateDressUri,
                    cancellationToken);
                seniorTeam.Region = region;

                await this.seniorTeamRepository.UpdateAsync(seniorTeam);
            }

            await this.databaseContext.SaveAsync();
        }
    }
}