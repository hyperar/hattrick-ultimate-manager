namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Chpp.Players;
    using Microsoft.EntityFrameworkCore;

    public class PlayersPersister : IFilePersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.SeniorPlayer> seniorPlayerRepository;

        private readonly IRepository<Domain.SeniorPlayerSkillSet> seniorPlayerSkillSetRepository;

        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public PlayersPersister(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Country> countryRepository,
            IHattrickRepository<Domain.SeniorPlayer> seniorPlayerRepository,
            IRepository<Domain.SeniorPlayerSkillSet> seniorPlayerSkillSetRepository,
            IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.seniorPlayerRepository = seniorPlayerRepository;
            this.seniorPlayerSkillSetRepository = seniorPlayerSkillSetRepository;
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public async Task PersistFileAsync(FileDownloadTaskBase fileDownloadTask, CancellationToken cancellationToken)
        {
            var xmlFileDownloadTask = fileDownloadTask as XmlFileDownloadTask;

            ArgumentNullException.ThrowIfNull(xmlFileDownloadTask);

            var players = xmlFileDownloadTask.Entity as HattrickData;

            ArgumentNullException.ThrowIfNull(players);
            ArgumentNullException.ThrowIfNull(players.Team.PlayerList);

            var seniorTeam = await this.seniorTeamRepository.GetByIdAsync(players.Team.TeamId);

            ArgumentNullException.ThrowIfNull(seniorTeam);

            var formerPlayerIds = await this.seniorPlayerRepository.Query(x => x.SeniorTeamHattrickId == seniorTeam.HattrickId)
                .Select(x => x.HattrickId)
                .Except(
                    players.Team.PlayerList.Select(x => x.PlayerId))
                .ToArrayAsync(cancellationToken);

            if (formerPlayerIds.Length > 0)
            {
                await this.seniorPlayerRepository.DeleteRangeAsync(formerPlayerIds);
            }

            foreach (var curPlayer in players.Team.PlayerList)
            {
                await this.PersistPlayerNodeAsync(
                    curPlayer,
                    seniorTeam,
                    cancellationToken);
            }
        }

        private async Task<Domain.SeniorPlayer> PersistPlayerNodeAsync(Player playerNode, Domain.SeniorTeam seniorTeam, CancellationToken cancellationToken)
        {
            var seniorPlayer = await this.seniorPlayerRepository.GetByIdAsync(playerNode.PlayerId);

            var country = await this.countryRepository.GetByIdAsync(
                    playerNode.CountryId);

            ArgumentNullException.ThrowIfNull(country);

            if (seniorPlayer == null)
            {
                seniorPlayer = new Domain.SeniorPlayer
                {
                    HattrickId = playerNode.PlayerId,
                    FirstName = playerNode.FirstName,
                    NickName = string.IsNullOrWhiteSpace(playerNode.NickName) ? null : playerNode.NickName,
                    LastName = playerNode.LastName,
                    AgeYears = playerNode.Age,
                    AgeDays = playerNode.AgeDays,
                    ShirtNumber = playerNode.PlayerNumber,
                    JoinedOn = playerNode.ArrivalDate,
                    HasMotherClubBonus = playerNode.MotherClubBonus,
                    Notes = string.IsNullOrWhiteSpace(playerNode.OwnerNotes) ? null : playerNode.OwnerNotes,
                    Statement = string.IsNullOrWhiteSpace(playerNode.Statement) ? null : playerNode.Statement,
                    Salary = playerNode.Salary,
                    Agreeability = (AgreeabilityLevel)playerNode.Agreeability,
                    Aggressiveness = (AggressivenessLevel)playerNode.Aggressiveness,
                    Honesty = (HonestyLevel)playerNode.Honesty,
                    Leadership = (LeadershipSkillLevel)playerNode.Leadership,
                    Specialty = (PlayerSpecialty)playerNode.Specialty,
                    BookingStatus = playerNode.Cards,
                    HealthStatus = playerNode.InjuryLevel,
                    Category = playerNode.PlayerCategoryId,
                    JuniorNationalTeamMatches = playerNode.CapsU20,
                    SeniorNationalTeamMatches = playerNode.Caps,
                    TeamMatches = playerNode.MatchesCurrentTeam,
                    TeamGoals = playerNode.GoalsCurrentTeam,
                    TeamAssists = playerNode.AssistsCurrentTeam,
                    SeasonSeriesGoals = playerNode.LeagueGoals,
                    SeasonCupGoals = playerNode.CupGoals,
                    SeasonFriendlyGoals = playerNode.FriendliesGoals,
                    CareerGoals = playerNode.CareerGoals,
                    CareerHattricks = playerNode.CareerHattricks,
                    CareerAssists = playerNode.CareerAssists,
                    Country = country,
                    SeniorTeam = seniorTeam
                };

                await this.seniorPlayerRepository.InsertAsync(
                    seniorPlayer);
            }
            else
            {
                seniorPlayer.FirstName = playerNode.FirstName;
                seniorPlayer.NickName = string.IsNullOrWhiteSpace(playerNode.NickName) ? null : playerNode.NickName;
                seniorPlayer.LastName = playerNode.LastName;
                seniorPlayer.AgeYears = playerNode.Age;
                seniorPlayer.AgeDays = playerNode.AgeDays;
                seniorPlayer.ShirtNumber = playerNode.PlayerNumber;
                seniorPlayer.JoinedOn = playerNode.ArrivalDate;
                seniorPlayer.HasMotherClubBonus = playerNode.MotherClubBonus;
                seniorPlayer.Notes = string.IsNullOrWhiteSpace(playerNode.OwnerNotes) ? null : playerNode.OwnerNotes;
                seniorPlayer.Statement = string.IsNullOrWhiteSpace(playerNode.Statement) ? null : playerNode.Statement;
                seniorPlayer.Salary = playerNode.Salary;
                seniorPlayer.BookingStatus = playerNode.Cards;
                seniorPlayer.HealthStatus = playerNode.InjuryLevel;
                seniorPlayer.Category = playerNode.PlayerCategoryId;
                seniorPlayer.JuniorNationalTeamMatches = playerNode.CapsU20;
                seniorPlayer.SeniorNationalTeamMatches = playerNode.Caps;
                seniorPlayer.TeamMatches = playerNode.MatchesCurrentTeam;
                seniorPlayer.TeamGoals = playerNode.GoalsCurrentTeam;
                seniorPlayer.TeamAssists = playerNode.AssistsCurrentTeam;
                seniorPlayer.SeasonSeriesGoals = playerNode.LeagueGoals;
                seniorPlayer.SeasonCupGoals = playerNode.CupGoals;
                seniorPlayer.SeasonFriendlyGoals = playerNode.FriendliesGoals;
                seniorPlayer.CareerGoals = playerNode.CareerGoals;
                seniorPlayer.CareerHattricks = playerNode.CareerHattricks;
                seniorPlayer.CareerAssists = playerNode.CareerAssists;

                await this.seniorPlayerRepository.UpdateAsync(seniorPlayer);
            }

            await this.databaseContext.SaveAsync();

            await this.PersistPlayerSkillsAsync(
                playerNode,
                seniorPlayer,
                seniorTeam,
                cancellationToken);

            return seniorPlayer;
        }

        private async Task PersistPlayerSkillsAsync(
            Player playerNode,
            Domain.SeniorPlayer seniorPlayer,
            Domain.SeniorTeam seniorTeam,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(playerNode.StaminaSkill);
            ArgumentNullException.ThrowIfNull(playerNode.KeeperSkill);
            ArgumentNullException.ThrowIfNull(playerNode.DefenderSkill);
            ArgumentNullException.ThrowIfNull(playerNode.PlaymakerSkill);
            ArgumentNullException.ThrowIfNull(playerNode.PassingSkill);
            ArgumentNullException.ThrowIfNull(playerNode.WingerSkill);
            ArgumentNullException.ThrowIfNull(playerNode.ScorerSkill);
            ArgumentNullException.ThrowIfNull(playerNode.SetPiecesSkill);

            var seniorPlayerSkillSet = await this.seniorPlayerSkillSetRepository.Query(
                    x => x.SeniorPlayerHattrickId == playerNode.PlayerId
                      && x.Season == seniorTeam.League.Season
                      && x.Week == seniorTeam.League.Week)
                    .SingleOrDefaultAsync(cancellationToken);

            if (seniorPlayerSkillSet == null)
            {
                seniorPlayerSkillSet = new Domain.SeniorPlayerSkillSet
                {
                    Season = seniorTeam.League.Season,
                    Week = seniorTeam.League.Week,
                    Form = (PlayerSkillLevel)playerNode.PlayerForm,
                    Stamina = (PlayerSkillLevel)playerNode.StaminaSkill.Value,
                    Keeper = (PlayerSkillLevel)playerNode.KeeperSkill.Value,
                    Defender = (PlayerSkillLevel)playerNode.DefenderSkill.Value,
                    Playmaking = (PlayerSkillLevel)playerNode.PlaymakerSkill.Value,
                    Winger = (PlayerSkillLevel)playerNode.WingerSkill.Value,
                    Passing = (PlayerSkillLevel)playerNode.PassingSkill.Value,
                    Scoring = (PlayerSkillLevel)playerNode.ScorerSkill.Value,
                    SetPieces = (PlayerSkillLevel)playerNode.ScorerSkill.Value,
                    Loyalty = (PlayerSkillLevel)playerNode.Loyalty,
                    Experience = (PlayerSkillLevel)playerNode.Experience,
                    TotalSkillIndex = playerNode.TSI,
                    SeniorPlayer = seniorPlayer
                };

                await this.seniorPlayerSkillSetRepository.InsertAsync(seniorPlayerSkillSet);
            }
            else
            {
                seniorPlayerSkillSet.Form = (PlayerSkillLevel)playerNode.PlayerForm;
                seniorPlayerSkillSet.Stamina = (PlayerSkillLevel)playerNode.StaminaSkill.Value;
                seniorPlayerSkillSet.Keeper = (PlayerSkillLevel)playerNode.KeeperSkill.Value;
                seniorPlayerSkillSet.Defender = (PlayerSkillLevel)playerNode.DefenderSkill.Value;
                seniorPlayerSkillSet.Playmaking = (PlayerSkillLevel)playerNode.PlaymakerSkill.Value;
                seniorPlayerSkillSet.Winger = (PlayerSkillLevel)playerNode.WingerSkill.Value;
                seniorPlayerSkillSet.Passing = (PlayerSkillLevel)playerNode.PassingSkill.Value;
                seniorPlayerSkillSet.Scoring = (PlayerSkillLevel)playerNode.ScorerSkill.Value;
                seniorPlayerSkillSet.SetPieces = (PlayerSkillLevel)playerNode.ScorerSkill.Value;
                seniorPlayerSkillSet.Loyalty = (PlayerSkillLevel)playerNode.Loyalty;
                seniorPlayerSkillSet.Experience = (PlayerSkillLevel)playerNode.Experience;
                seniorPlayerSkillSet.TotalSkillIndex = playerNode.TSI;

                await this.seniorPlayerSkillSetRepository.UpdateAsync(seniorPlayerSkillSet);
            }
        }
    }
}