namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Chpp;
    using Hyperar.HUM.Shared.Models.Chpp.WorldDetails;
    using Microsoft.EntityFrameworkCore;

    public class WorldDetailsPersister : IFilePersisterStrategy
    {
        private readonly IHattrickRepository<Domain.Country> countryRepository;

        private readonly IRepository<Domain.Currency> currencyRepository;

        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickRepository<Domain.LeagueCup> leagueCupRepository;

        private readonly IHattrickRepository<Domain.League> leagueRepository;

        private readonly IHattrickRepository<Domain.Region> regionRepository;

        public WorldDetailsPersister(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Country> countryRepository,
            IRepository<Domain.Currency> currencyRepository,
            IHattrickRepository<Domain.LeagueCup> leagueCupRepository,
            IHattrickRepository<Domain.League> leagueRepository,
            IHattrickRepository<Domain.Region> regionRepository)
        {
            this.databaseContext = databaseContext;
            this.countryRepository = countryRepository;
            this.currencyRepository = currencyRepository;
            this.leagueCupRepository = leagueCupRepository;
            this.leagueRepository = leagueRepository;
            this.regionRepository = regionRepository;
        }

        public async Task PersistFileAsync(FileDownloadTaskBase fileDownloadTask, CancellationToken cancellationToken)
        {
            var xmlFileDownloadTask = fileDownloadTask as XmlFileDownloadTask;

            ArgumentNullException.ThrowIfNull(xmlFileDownloadTask);

            var worldDetails = xmlFileDownloadTask.Entity as HattrickData;

            ArgumentNullException.ThrowIfNull(worldDetails);

            foreach (var curLeagueNode in worldDetails.LeagueList)
            {
                await this.PersistLeagueNodeAsync(curLeagueNode, cancellationToken);
            }
        }

        private async Task PersistCountryNodeAsync(
            Country countryNode,
            Domain.League league,
            CancellationToken cancellationToken)
        {
            if (!countryNode.Available)
            {
                return;
            }

            ArgumentNullException.ThrowIfNull(countryNode.CountryId);
            ArgumentNullException.ThrowIfNull(countryNode.CountryName);
            ArgumentNullException.ThrowIfNull(countryNode.CurrencyName);
            ArgumentNullException.ThrowIfNull(countryNode.CurrencyRate);
            ArgumentNullException.ThrowIfNull(countryNode.CountryCode);
            ArgumentNullException.ThrowIfNull(countryNode.DateFormat);
            ArgumentNullException.ThrowIfNull(countryNode.TimeFormat);

            var currency = await this.PersistCurrencyAsync(
                countryNode.CurrencyName,
                countryNode.CurrencyRate.Value,
                cancellationToken);

            var country = await this.countryRepository.GetByIdAsync(countryNode.CountryId.Value);

            country ??= await this.countryRepository.InsertAsync(new Domain.Country
            {
                HattrickId = countryNode.CountryId.Value,
                Name = countryNode.CountryName,
                Code = countryNode.CountryCode,
                DateFormat = countryNode.DateFormat,
                TimeFormat = countryNode.TimeFormat,
                Currency = currency,
                League = league
            });

            if (countryNode.RegionList is not null)
            {
                foreach (var curRegionNode in countryNode.RegionList)
                {
                    await this.PersistRegionNodeAsync(curRegionNode, country);
                }
            }
        }

        private async Task PersistCupNodeAsync(
            Cup cupNode,
            Domain.League league)
        {
            var leagueCup = await this.leagueCupRepository.GetByIdAsync(cupNode.CupId);

            if (leagueCup is null)
            {
                await this.leagueCupRepository.InsertAsync(new Domain.LeagueCup
                {
                    HattrickId = cupNode.CupId,
                    Name = cupNode.CupName,
                    Type = (LeagueCupType)cupNode.CupLevel,
                    SubType = (LeagueCupSubType)cupNode.CupLevelIndex,
                    Level = cupNode.CupLeagueLevel,
                    Week = cupNode.MatchRound,
                    WeeksLeft = cupNode.MatchRoundsLeft,
                    League = league
                });
            }
        }

        private async Task<Domain.Currency> PersistCurrencyAsync(
            string currencyName,
            decimal currencyRate,
            CancellationToken cancellationToken)
        {
            var currency = await this.currencyRepository.Query(x => x.Name == currencyName
                                                                 && x.ConvertionRate == currencyRate)
                .SingleOrDefaultAsync(cancellationToken);

            if (currency is null)
            {
                currency = await this.currencyRepository.InsertAsync(new Domain.Currency
                {
                    Name = currencyName,
                    ConvertionRate = currencyRate
                });

                await this.databaseContext.SaveAsync();
            }

            return currency;
        }

        private async Task PersistLeagueNodeAsync(
            League leagueNode,
            CancellationToken cancellationToken)
        {
            var league = await this.leagueRepository.GetByIdAsync(leagueNode.LeagueId);

            if (league is null)
            {
                league = await this.leagueRepository.InsertAsync(new Domain.League
                {
                    HattrickId = leagueNode.LeagueId,
                    Name = leagueNode.LeagueName,
                    ShortName = leagueNode.ShortName,
                    EnglishName = leagueNode.EnglishName,
                    Continent = leagueNode.Continent,
                    Zone = leagueNode.ZoneName,
                    Season = leagueNode.Season,
                    Week = leagueNode.MatchRound,
                    SeasonOffset = leagueNode.SeasonOffset,
                    SeniorNationalTeamHattrickId = leagueNode.NationalTeamId != 0 ? leagueNode.NationalTeamId : null,
                    JuniorNationalTeamHattrickId = leagueNode.U20TeamId != 0 ? leagueNode.U20TeamId : null,
                    ActiveTeams = leagueNode.ActiveTeams,
                    ActiveUsers = leagueNode.ActiveUsers,
                    WaitingUsers = leagueNode.WaitingUsers,
                    LeagueLevels = leagueNode.NumberOfLevels,
                    NextTrainingUpdate = leagueNode.TrainingDate,
                    NextEconomyUpdate = leagueNode.EconomyDate,
                    NextSeriesMatchDate = leagueNode.SeriesMatchDate,
                    NextCupMatchDate = leagueNode.CupMatchDate,
                    FirstDailyUpdate = leagueNode.Sequence1,
                    SecondDailyUpdate = leagueNode.Sequence2,
                    ThirdDailyUpdate = leagueNode.Sequence3,
                    FourthDailyUpdate = leagueNode.Sequence5,
                    FifthDailyUpdate = leagueNode.Sequence7,
                    FlagBytes = await ImageHelper.ReadFileFromCacheAsync(
                        string.Format(
                            ImageHelper.FlagUrlMask,
                            leagueNode.LeagueId),
                        cancellationToken),
                    InactiveFlagBytes = await ImageHelper.ReadFileFromCacheAsync(
                        string.Format(
                            ImageHelper.InactiveFlagUrlMask,
                            leagueNode.LeagueId),
                        cancellationToken),
                });

                await this.leagueRepository.InsertAsync(league);
            }
            else
            {
                league.Season = leagueNode.Season;
                league.Week = leagueNode.MatchRound;
                league.ActiveTeams = leagueNode.ActiveTeams;
                league.ActiveUsers = leagueNode.ActiveUsers;
                league.WaitingUsers = leagueNode.WaitingUsers;
                league.LeagueLevels = leagueNode.NumberOfLevels;
                league.NextTrainingUpdate = leagueNode.TrainingDate;
                league.NextEconomyUpdate = leagueNode.EconomyDate;
                league.NextSeriesMatchDate = leagueNode.SeriesMatchDate;
                league.NextCupMatchDate = leagueNode.CupMatchDate;
                league.FirstDailyUpdate = leagueNode.Sequence1;
                league.SecondDailyUpdate = leagueNode.Sequence2;
                league.ThirdDailyUpdate = leagueNode.Sequence3;
                league.FourthDailyUpdate = leagueNode.Sequence5;
                league.FifthDailyUpdate = leagueNode.Sequence7;

                await this.leagueRepository.UpdateAsync(league);
            }

            foreach (var curCupNode in leagueNode.Cups)
            {
                await this.PersistCupNodeAsync(
                    curCupNode,
                    league);
            }

            await this.PersistCountryNodeAsync(leagueNode.Country, league, cancellationToken);
        }

        private async Task PersistRegionNodeAsync(
            IdName regionNode,
            Domain.Country country)
        {
            var region = await this.regionRepository.GetByIdAsync(regionNode.Id);

            if (region is null)
            {
                await this.regionRepository.InsertAsync(new Domain.Region
                {
                    HattrickId = regionNode.Id,
                    Name = regionNode.Name,
                    Country = country
                });
            }
        }
    }
}