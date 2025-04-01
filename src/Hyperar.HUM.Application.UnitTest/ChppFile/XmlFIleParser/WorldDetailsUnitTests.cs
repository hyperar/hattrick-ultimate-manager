namespace Hyperar.HUM.Application.UnitTest.ChppFile.XmlFIleParser
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Shared.Models.Chpp;
    using Hyperar.HUM.Shared.Models.Chpp.WorldDetails;
    using Hyperar.HUM.Test.Shared;
    using Microsoft.Extensions.DependencyInjection;

    public class WorldDetailsUnitTests : XmlFileParserBase, IClassFixture<ServicesFixture>
    {
        private const string WorldDetailsCountry = "Assets\\Xml\\WorldDetails\\WorldDetails_Country.xml";

        private const string WorldDetailsMultipleLeague = "Assets\\Xml\\WorldDetails\\WorldDetails_MultipleLeague.xml";

        private const string WorldDetailsNoCountry = "Assets\\Xml\\WorldDetails\\WorldDetails_NoCountry.xml";

        private const string WorldDetailsRegions = "Assets\\Xml\\WorldDetails\\WorldDetails_Regions.xml";

        private readonly IXmlFileParser xmlFileParser;

        public WorldDetailsUnitTests(ServicesFixture fixture)
        {
            this.xmlFileParser = fixture.Services.GetRequiredService<IXmlFileParser>();
        }

        [Fact]
        public async Task WorldDetails_Country_ShouldBeEqual()
        {
            var fileContent = await OpenFile(WorldDetailsCountry);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            Assert.Equal("worlddetails.xml", result.FileName);
            Assert.Equal(1.9m, result.Version);
            Assert.Equal(12345678, result.UserId);
            Assert.Equal(new DateTime(2025, 1, 1, 8, 15, 30), result.FetchedDate);

            Assert.NotNull(result.LeagueList);
            Assert.NotEmpty(result.LeagueList);
            Assert.Single(result.LeagueList);

            Assert.Equal(7, result.LeagueList.First().LeagueId);
            Assert.Equal("Argentina", result.LeagueList.First().LeagueName);
            Assert.Equal(90, result.LeagueList.First().Season);
            Assert.Equal(0, result.LeagueList.First().SeasonOffset);
            Assert.Equal(13, result.LeagueList.First().MatchRound);
            Assert.Equal("Argentina", result.LeagueList.First().ShortName);
            Assert.Equal("South America", result.LeagueList.First().Continent);
            Assert.Equal("South America", result.LeagueList.First().ZoneName);
            Assert.Equal("Argentina", result.LeagueList.First().EnglishName);
            Assert.Equal(51, result.LeagueList.First().LanguageId);
            Assert.Equal("Español, Rioplatense", result.LeagueList.First().LanguageName);

            Assert.NotNull(result.LeagueList.First().Country);
            Assert.True(result.LeagueList.First().Country.Available);
            Assert.Equal(7, result.LeagueList.First().Country.CountryId);
            Assert.Equal("Argentina", result.LeagueList.First().Country.CountryName);
            Assert.Equal("Pesos", result.LeagueList.First().Country.CurrencyName);
            Assert.Equal(10m, result.LeagueList.First().Country.CurrencyRate);
            Assert.Equal("AR", result.LeagueList.First().Country.CountryCode);
            Assert.Equal("DD/MM/YYYY", result.LeagueList.First().Country.DateFormat);
            Assert.Equal("hh:mm", result.LeagueList.First().Country.TimeFormat);

            Assert.NotEmpty(result.LeagueList.First().Cups);
            Assert.Equal(10, result.LeagueList.First().Cups.Length);

            Assert.Equal(
                new Cup(2, "Copa Argentina Diego Armando Maradona", 0, 1, 1, 12, 1),
                result.LeagueList.First().Cups[0]);

            Assert.Equal(
                new Cup(518, "Copa Esmeralda Pedro 'Apache' Zurita", 0, 2, 1, 11, 2),
                result.LeagueList.First().Cups[1]);

            Assert.Equal(
                new Cup(647, "Copa Rubí Alberto Conte", 0, 2, 2, 10, 2),
                result.LeagueList.First().Cups[2]);

            Assert.Equal(
                new Cup(776, "Copa Zafiro caPiTAn_piLuSO", 0, 2, 3, 9, 2),
                result.LeagueList.First().Cups[3]);

            Assert.Equal(
                new Cup(905, "Copa Consuelo Gerardo Raúl Peludo", 0, 3, 1, 10, 2),
                result.LeagueList.First().Cups[4]);

            Assert.Equal(
                new Cup(1329, "Copa de VII División", 7, 1, 1, 7, 0),
                result.LeagueList.First().Cups[5]);

            Assert.Equal(
                new Cup(1342, "Copa Desafío Esmeralda VII División", 7, 2, 1, 7, 0),
                result.LeagueList.First().Cups[6]);

            Assert.Equal(
                new Cup(1355, "Copa Desafío Rubí VII División ", 7, 2, 2, 6, 0),
                result.LeagueList.First().Cups[7]);

            Assert.Equal(
                new Cup(1368, "Copa Desafío Zafiro VII División ", 7, 2, 3, 5, 0),
                result.LeagueList.First().Cups[8]);

            Assert.Equal(
                new Cup(1381, "Copa Consuelo VII División", 7, 3, 1, 6, 0),
                result.LeagueList.First().Cups[9]);

            Assert.Equal(3006, result.LeagueList.First().NationalTeamId);
            Assert.Equal(3047, result.LeagueList.First().U20TeamId);
            Assert.Equal(6751, result.LeagueList.First().ActiveTeams);
            Assert.Equal(6646, result.LeagueList.First().ActiveUsers);
            Assert.Equal(0, result.LeagueList.First().WaitingUsers);
            Assert.Equal(new DateTime(2025, 4, 4, 2, 0, 0), result.LeagueList.First().TrainingDate);
            Assert.Equal(new DateTime(2025, 4, 5, 3, 30, 0), result.LeagueList.First().EconomyDate);
            Assert.Equal(new DateTime(2025, 4, 2, 23, 10, 0), result.LeagueList.First().CupMatchDate);
            Assert.Equal(new DateTime(2025, 4, 6, 21, 40, 0), result.LeagueList.First().SeriesMatchDate);
            Assert.Equal(new DateTime(2025, 4, 7, 11, 0, 0), result.LeagueList.First().Sequence1);
            Assert.Equal(new DateTime(2025, 4, 8, 11, 0, 0), result.LeagueList.First().Sequence2);
            Assert.Equal(new DateTime(2025, 4, 1, 21, 40, 0), result.LeagueList.First().Sequence3);
            Assert.Equal(new DateTime(2025, 4, 3, 11, 0, 0), result.LeagueList.First().Sequence5);
            Assert.Equal(new DateTime(2025, 4, 4, 19, 20, 0), result.LeagueList.First().Sequence7);
            Assert.Equal(6, result.LeagueList.First().NumberOfLevels);
        }

        [Fact]
        public async Task WorldDetails_MultipleLeague_ShouldBeEqual()
        {
            var fileContent = await OpenFile(WorldDetailsMultipleLeague);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            Assert.Equal("worlddetails.xml", result.FileName);
            Assert.Equal(1.9m, result.Version);
            Assert.Equal(12345678, result.UserId);
            Assert.Equal(new DateTime(2025, 1, 1, 8, 15, 30), result.FetchedDate);

            Assert.NotNull(result.LeagueList);
            Assert.NotEmpty(result.LeagueList);
            Assert.Equal(2, result.LeagueList.Length);

            Assert.Equal(7, result.LeagueList.First().LeagueId);
            Assert.Equal("Argentina", result.LeagueList.First().LeagueName);
            Assert.Equal(90, result.LeagueList.First().Season);
            Assert.Equal(0, result.LeagueList.First().SeasonOffset);
            Assert.Equal(13, result.LeagueList.First().MatchRound);
            Assert.Equal("Argentina", result.LeagueList.First().ShortName);
            Assert.Equal("South America", result.LeagueList.First().Continent);
            Assert.Equal("South America", result.LeagueList.First().ZoneName);
            Assert.Equal("Argentina", result.LeagueList.First().EnglishName);
            Assert.Equal(51, result.LeagueList.First().LanguageId);
            Assert.Equal("Español, Rioplatense", result.LeagueList.First().LanguageName);

            Assert.NotNull(result.LeagueList.First().Country);
            Assert.True(result.LeagueList.First().Country.Available);
            Assert.Equal(7, result.LeagueList.First().Country.CountryId);
            Assert.Equal("Argentina", result.LeagueList.First().Country.CountryName);
            Assert.Equal("Pesos", result.LeagueList.First().Country.CurrencyName);
            Assert.Equal(10m, result.LeagueList.First().Country.CurrencyRate);
            Assert.Equal("AR", result.LeagueList.First().Country.CountryCode);
            Assert.Equal("DD/MM/YYYY", result.LeagueList.First().Country.DateFormat);
            Assert.Equal("hh:mm", result.LeagueList.First().Country.TimeFormat);

            var regions = result.LeagueList.First().Country.RegionList;

            Assert.NotNull(regions);
            Assert.NotEmpty(regions);
            Assert.Equal(24, regions.Length);

            Assert.Equal(new IdName(67, "Catamarca"), regions[0]);
            Assert.Equal(new IdName(68, "Chaco"), regions[1]);
            Assert.Equal(new IdName(69, "Chubut"), regions[2]);
            Assert.Equal(new IdName(70, "Ciudad Autónoma de Buenos Aires"), regions[3]);
            Assert.Equal(new IdName(71, "Córdoba"), regions[4]);
            Assert.Equal(new IdName(72, "Corrientes"), regions[5]);
            Assert.Equal(new IdName(73, "Entre Ríos"), regions[6]);
            Assert.Equal(new IdName(74, "Formosa"), regions[7]);
            Assert.Equal(new IdName(75, "Jujuy"), regions[8]);
            Assert.Equal(new IdName(76, "La Pampa"), regions[9]);
            Assert.Equal(new IdName(77, "La Rioja"), regions[10]);
            Assert.Equal(new IdName(78, "Mendoza"), regions[11]);
            Assert.Equal(new IdName(79, "Misiones"), regions[12]);
            Assert.Equal(new IdName(80, "Neuquén"), regions[13]);
            Assert.Equal(new IdName(66, "Provincia de Buenos Aires"), regions[14]);
            Assert.Equal(new IdName(81, "Río Negro"), regions[15]);
            Assert.Equal(new IdName(82, "Salta"), regions[16]);
            Assert.Equal(new IdName(83, "San Juan"), regions[17]);
            Assert.Equal(new IdName(84, "San Luis"), regions[18]);
            Assert.Equal(new IdName(85, "Santa Cruz"), regions[19]);
            Assert.Equal(new IdName(86, "Santa Fe"), regions[20]);
            Assert.Equal(new IdName(87, "Santiago del Estero"), regions[21]);
            Assert.Equal(new IdName(88, "Tierra del Fuego, Antártida e Islas del Atlántico "), regions[22]);
            Assert.Equal(new IdName(89, "Tucumán"), regions[23]);

            Assert.NotEmpty(result.LeagueList.First().Cups);
            Assert.Equal(10, result.LeagueList.First().Cups.Length);

            Assert.Equal(
                new Cup(2, "Copa Argentina Diego Armando Maradona", 0, 1, 1, 12, 1),
                result.LeagueList.First().Cups[0]);

            Assert.Equal(
                new Cup(518, "Copa Esmeralda Pedro 'Apache' Zurita", 0, 2, 1, 11, 2),
                result.LeagueList.First().Cups[1]);

            Assert.Equal(
                new Cup(647, "Copa Rubí Alberto Conte", 0, 2, 2, 10, 2),
                result.LeagueList.First().Cups[2]);

            Assert.Equal(
                new Cup(776, "Copa Zafiro caPiTAn_piLuSO", 0, 2, 3, 9, 2),
                result.LeagueList.First().Cups[3]);

            Assert.Equal(
                new Cup(905, "Copa Consuelo Gerardo Raúl Peludo", 0, 3, 1, 10, 2),
                result.LeagueList.First().Cups[4]);

            Assert.Equal(
                new Cup(1329, "Copa de VII División", 7, 1, 1, 7, 0),
                result.LeagueList.First().Cups[5]);

            Assert.Equal(
                new Cup(1342, "Copa Desafío Esmeralda VII División", 7, 2, 1, 7, 0),
                result.LeagueList.First().Cups[6]);

            Assert.Equal(
                new Cup(1355, "Copa Desafío Rubí VII División ", 7, 2, 2, 6, 0),
                result.LeagueList.First().Cups[7]);

            Assert.Equal(
                new Cup(1368, "Copa Desafío Zafiro VII División ", 7, 2, 3, 5, 0),
                result.LeagueList.First().Cups[8]);

            Assert.Equal(
                new Cup(1381, "Copa Consuelo VII División", 7, 3, 1, 6, 0),
                result.LeagueList.First().Cups[9]);

            Assert.Equal(3006, result.LeagueList.First().NationalTeamId);
            Assert.Equal(3047, result.LeagueList.First().U20TeamId);
            Assert.Equal(6751, result.LeagueList.First().ActiveTeams);
            Assert.Equal(6646, result.LeagueList.First().ActiveUsers);
            Assert.Equal(0, result.LeagueList.First().WaitingUsers);
            Assert.Equal(new DateTime(2025, 4, 4, 2, 0, 0), result.LeagueList.First().TrainingDate);
            Assert.Equal(new DateTime(2025, 4, 5, 3, 30, 0), result.LeagueList.First().EconomyDate);
            Assert.Equal(new DateTime(2025, 4, 2, 23, 10, 0), result.LeagueList.First().CupMatchDate);
            Assert.Equal(new DateTime(2025, 4, 6, 21, 40, 0), result.LeagueList.First().SeriesMatchDate);
            Assert.Equal(new DateTime(2025, 4, 7, 11, 0, 0), result.LeagueList.First().Sequence1);
            Assert.Equal(new DateTime(2025, 4, 8, 11, 0, 0), result.LeagueList.First().Sequence2);
            Assert.Equal(new DateTime(2025, 4, 1, 21, 40, 0), result.LeagueList.First().Sequence3);
            Assert.Equal(new DateTime(2025, 4, 3, 11, 0, 0), result.LeagueList.First().Sequence5);
            Assert.Equal(new DateTime(2025, 4, 4, 19, 20, 0), result.LeagueList.First().Sequence7);
            Assert.Equal(6, result.LeagueList.First().NumberOfLevels);

            Assert.Equal(1003, result.LeagueList.Last().LeagueId);
            Assert.Equal("Homegrown League", result.LeagueList.Last().LeagueName);
            Assert.Equal(3, result.LeagueList.Last().Season);
            Assert.Equal(-87, result.LeagueList.Last().SeasonOffset);
            Assert.Equal(13, result.LeagueList.Last().MatchRound);
            Assert.Equal("Homegrown", result.LeagueList.Last().ShortName);
            Assert.Equal("World", result.LeagueList.Last().Continent);
            Assert.Equal("World", result.LeagueList.Last().ZoneName);
            Assert.Equal("Homegrown League", result.LeagueList.Last().EnglishName);
            Assert.Equal(2, result.LeagueList.Last().LanguageId);
            Assert.Equal("English (UK)", result.LeagueList.Last().LanguageName);

            Assert.NotNull(result.LeagueList.Last().Country);
            Assert.False(result.LeagueList.Last().Country.Available);
            Assert.Null(result.LeagueList.Last().Country.CountryId);
            Assert.Null(result.LeagueList.Last().Country.CountryName);
            Assert.Null(result.LeagueList.Last().Country.CurrencyName);
            Assert.Null(result.LeagueList.Last().Country.CurrencyRate);
            Assert.Null(result.LeagueList.Last().Country.CountryCode);
            Assert.Null(result.LeagueList.Last().Country.DateFormat);
            Assert.Null(result.LeagueList.Last().Country.TimeFormat);

            Assert.NotEmpty(result.LeagueList.Last().Cups);
            Assert.Equal(5, result.LeagueList.Last().Cups.Length);

            Assert.Equal(
                new Cup(1553, "Homegrown Cup", 0, 1, 1, 12, 0),
                result.LeagueList.Last().Cups[0]);

            Assert.Equal(
                new Cup(1554, "Homegrown Emerald Cup", 0, 2, 1, 11, 1),
                result.LeagueList.Last().Cups[1]);

            Assert.Equal(
                new Cup(1555, "Homegrown Ruby Cup", 0, 2, 2, 10, 1),
                result.LeagueList.Last().Cups[2]);

            Assert.Equal(
                new Cup(1556, "Homegrown Sapphire Cup", 0, 2, 3, 9, 1),
                result.LeagueList.Last().Cups[3]);

            Assert.Equal(
                new Cup(1557, "Homegrown Consolation Cup", 0, 3, 1, 10, 1),
                result.LeagueList.Last().Cups[4]);

            Assert.Equal(0, result.LeagueList.Last().NationalTeamId);
            Assert.Equal(0, result.LeagueList.Last().U20TeamId);
            Assert.Equal(2233, result.LeagueList.Last().ActiveTeams);
            Assert.Equal(0, result.LeagueList.Last().ActiveUsers);
            Assert.Equal(0, result.LeagueList.Last().WaitingUsers);
            Assert.Equal(new DateTime(2025, 4, 4, 1, 30, 0), result.LeagueList.Last().TrainingDate);
            Assert.Equal(new DateTime(2025, 4, 6, 2, 5, 0), result.LeagueList.Last().EconomyDate);
            Assert.Equal(new DateTime(2025, 3, 26, 19, 45, 0), result.LeagueList.Last().CupMatchDate);
            Assert.Equal(new DateTime(2025, 4, 6, 19, 45, 0), result.LeagueList.Last().SeriesMatchDate);
            Assert.Equal(new DateTime(2025, 4, 7, 8, 20, 0), result.LeagueList.Last().Sequence1);
            Assert.Equal(new DateTime(2025, 4, 8, 8, 20, 0), result.LeagueList.Last().Sequence2);
            Assert.Equal(new DateTime(2025, 4, 8, 17, 20, 0), result.LeagueList.Last().Sequence3);
            Assert.Equal(new DateTime(2025, 4, 3, 8, 20, 0), result.LeagueList.Last().Sequence5);
            Assert.Equal(new DateTime(2025, 4, 5, 5, 0, 0), result.LeagueList.Last().Sequence7);
            Assert.Equal(6, result.LeagueList.Last().NumberOfLevels);
        }

        [Fact]
        public async Task WorldDetails_NoCountry_ShouldBeEqual()
        {
            var fileContent = await OpenFile(WorldDetailsNoCountry);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            Assert.Equal("worlddetails.xml", result.FileName);
            Assert.Equal(1.9m, result.Version);
            Assert.Equal(12345678, result.UserId);
            Assert.Equal(new DateTime(2025, 1, 1, 8, 15, 30), result.FetchedDate);

            Assert.NotNull(result.LeagueList);
            Assert.NotEmpty(result.LeagueList);
            Assert.Single(result.LeagueList);

            Assert.Equal(1003, result.LeagueList.First().LeagueId);
            Assert.Equal("Homegrown League", result.LeagueList.First().LeagueName);
            Assert.Equal(3, result.LeagueList.First().Season);
            Assert.Equal(-87, result.LeagueList.First().SeasonOffset);
            Assert.Equal(13, result.LeagueList.First().MatchRound);
            Assert.Equal("Homegrown", result.LeagueList.First().ShortName);
            Assert.Equal("World", result.LeagueList.First().Continent);
            Assert.Equal("World", result.LeagueList.First().ZoneName);
            Assert.Equal("Homegrown League", result.LeagueList.First().EnglishName);
            Assert.Equal(2, result.LeagueList.First().LanguageId);
            Assert.Equal("English (UK)", result.LeagueList.First().LanguageName);

            Assert.NotNull(result.LeagueList.First().Country);
            Assert.False(result.LeagueList.First().Country.Available);
            Assert.Null(result.LeagueList.First().Country.CountryId);
            Assert.Null(result.LeagueList.First().Country.CountryName);
            Assert.Null(result.LeagueList.First().Country.CurrencyName);
            Assert.Null(result.LeagueList.First().Country.CurrencyRate);
            Assert.Null(result.LeagueList.First().Country.CountryCode);
            Assert.Null(result.LeagueList.First().Country.DateFormat);
            Assert.Null(result.LeagueList.First().Country.TimeFormat);

            Assert.NotEmpty(result.LeagueList.First().Cups);
            Assert.Equal(5, result.LeagueList.First().Cups.Length);

            Assert.Equal(
                new Cup(1553, "Homegrown Cup", 0, 1, 1, 12, 0),
                result.LeagueList.First().Cups[0]);

            Assert.Equal(
                new Cup(1554, "Homegrown Emerald Cup", 0, 2, 1, 11, 1),
                result.LeagueList.First().Cups[1]);

            Assert.Equal(
                new Cup(1555, "Homegrown Ruby Cup", 0, 2, 2, 10, 1),
                result.LeagueList.First().Cups[2]);

            Assert.Equal(
                new Cup(1556, "Homegrown Sapphire Cup", 0, 2, 3, 9, 1),
                result.LeagueList.First().Cups[3]);

            Assert.Equal(
                new Cup(1557, "Homegrown Consolation Cup", 0, 3, 1, 10, 1),
                result.LeagueList.First().Cups[4]);

            Assert.Equal(0, result.LeagueList.First().NationalTeamId);
            Assert.Equal(0, result.LeagueList.First().U20TeamId);
            Assert.Equal(2233, result.LeagueList.First().ActiveTeams);
            Assert.Equal(0, result.LeagueList.First().ActiveUsers);
            Assert.Equal(0, result.LeagueList.First().WaitingUsers);
            Assert.Equal(new DateTime(2025, 4, 4, 1, 30, 0), result.LeagueList.First().TrainingDate);
            Assert.Equal(new DateTime(2025, 4, 6, 2, 5, 0), result.LeagueList.First().EconomyDate);
            Assert.Equal(new DateTime(2025, 3, 26, 19, 45, 0), result.LeagueList.First().CupMatchDate);
            Assert.Equal(new DateTime(2025, 4, 6, 19, 45, 0), result.LeagueList.First().SeriesMatchDate);
            Assert.Equal(new DateTime(2025, 4, 7, 8, 20, 0), result.LeagueList.First().Sequence1);
            Assert.Equal(new DateTime(2025, 4, 8, 8, 20, 0), result.LeagueList.First().Sequence2);
            Assert.Equal(new DateTime(2025, 4, 8, 17, 20, 0), result.LeagueList.First().Sequence3);
            Assert.Equal(new DateTime(2025, 4, 3, 8, 20, 0), result.LeagueList.First().Sequence5);
            Assert.Equal(new DateTime(2025, 4, 5, 5, 0, 0), result.LeagueList.First().Sequence7);
            Assert.Equal(6, result.LeagueList.First().NumberOfLevels);
        }

        [Fact]
        public async Task WorldDetails_Regions_ShouldBeEqual()
        {
            var fileContent = await OpenFile(WorldDetailsRegions);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            Assert.Equal("worlddetails.xml", result.FileName);
            Assert.Equal(1.9m, result.Version);
            Assert.Equal(12345678, result.UserId);
            Assert.Equal(new DateTime(2025, 1, 1, 8, 15, 30), result.FetchedDate);

            Assert.NotNull(result.LeagueList);
            Assert.NotEmpty(result.LeagueList);
            Assert.Single(result.LeagueList);

            Assert.Equal(7, result.LeagueList.First().LeagueId);
            Assert.Equal("Argentina", result.LeagueList.First().LeagueName);
            Assert.Equal(90, result.LeagueList.First().Season);
            Assert.Equal(0, result.LeagueList.First().SeasonOffset);
            Assert.Equal(13, result.LeagueList.First().MatchRound);
            Assert.Equal("Argentina", result.LeagueList.First().ShortName);
            Assert.Equal("South America", result.LeagueList.First().Continent);
            Assert.Equal("South America", result.LeagueList.First().ZoneName);
            Assert.Equal("Argentina", result.LeagueList.First().EnglishName);
            Assert.Equal(51, result.LeagueList.First().LanguageId);
            Assert.Equal("Español, Rioplatense", result.LeagueList.First().LanguageName);

            Assert.NotNull(result.LeagueList.First().Country);
            Assert.True(result.LeagueList.First().Country.Available);
            Assert.Equal(7, result.LeagueList.First().Country.CountryId);
            Assert.Equal("Argentina", result.LeagueList.First().Country.CountryName);
            Assert.Equal("Pesos", result.LeagueList.First().Country.CurrencyName);
            Assert.Equal(10m, result.LeagueList.First().Country.CurrencyRate);
            Assert.Equal("AR", result.LeagueList.First().Country.CountryCode);
            Assert.Equal("DD/MM/YYYY", result.LeagueList.First().Country.DateFormat);
            Assert.Equal("hh:mm", result.LeagueList.First().Country.TimeFormat);

            var regions = result.LeagueList.First().Country.RegionList;

            Assert.NotNull(regions);
            Assert.NotEmpty(regions);
            Assert.Equal(24, regions.Length);

            Assert.Equal(new IdName(67, "Catamarca"), regions[0]);
            Assert.Equal(new IdName(68, "Chaco"), regions[1]);
            Assert.Equal(new IdName(69, "Chubut"), regions[2]);
            Assert.Equal(new IdName(70, "Ciudad Autónoma de Buenos Aires"), regions[3]);
            Assert.Equal(new IdName(71, "Córdoba"), regions[4]);
            Assert.Equal(new IdName(72, "Corrientes"), regions[5]);
            Assert.Equal(new IdName(73, "Entre Ríos"), regions[6]);
            Assert.Equal(new IdName(74, "Formosa"), regions[7]);
            Assert.Equal(new IdName(75, "Jujuy"), regions[8]);
            Assert.Equal(new IdName(76, "La Pampa"), regions[9]);
            Assert.Equal(new IdName(77, "La Rioja"), regions[10]);
            Assert.Equal(new IdName(78, "Mendoza"), regions[11]);
            Assert.Equal(new IdName(79, "Misiones"), regions[12]);
            Assert.Equal(new IdName(80, "Neuquén"), regions[13]);
            Assert.Equal(new IdName(66, "Provincia de Buenos Aires"), regions[14]);
            Assert.Equal(new IdName(81, "Río Negro"), regions[15]);
            Assert.Equal(new IdName(82, "Salta"), regions[16]);
            Assert.Equal(new IdName(83, "San Juan"), regions[17]);
            Assert.Equal(new IdName(84, "San Luis"), regions[18]);
            Assert.Equal(new IdName(85, "Santa Cruz"), regions[19]);
            Assert.Equal(new IdName(86, "Santa Fe"), regions[20]);
            Assert.Equal(new IdName(87, "Santiago del Estero"), regions[21]);
            Assert.Equal(new IdName(88, "Tierra del Fuego, Antártida e Islas del Atlántico "), regions[22]);
            Assert.Equal(new IdName(89, "Tucumán"), regions[23]);

            Assert.NotEmpty(result.LeagueList.First().Cups);
            Assert.Equal(10, result.LeagueList.First().Cups.Length);

            Assert.Equal(
                new Cup(2, "Copa Argentina Diego Armando Maradona", 0, 1, 1, 12, 1),
                result.LeagueList.First().Cups[0]);

            Assert.Equal(
                new Cup(518, "Copa Esmeralda Pedro 'Apache' Zurita", 0, 2, 1, 11, 2),
                result.LeagueList.First().Cups[1]);

            Assert.Equal(
                new Cup(647, "Copa Rubí Alberto Conte", 0, 2, 2, 10, 2),
                result.LeagueList.First().Cups[2]);

            Assert.Equal(
                new Cup(776, "Copa Zafiro caPiTAn_piLuSO", 0, 2, 3, 9, 2),
                result.LeagueList.First().Cups[3]);

            Assert.Equal(
                new Cup(905, "Copa Consuelo Gerardo Raúl Peludo", 0, 3, 1, 10, 2),
                result.LeagueList.First().Cups[4]);

            Assert.Equal(
                new Cup(1329, "Copa de VII División", 7, 1, 1, 7, 0),
                result.LeagueList.First().Cups[5]);

            Assert.Equal(
                new Cup(1342, "Copa Desafío Esmeralda VII División", 7, 2, 1, 7, 0),
                result.LeagueList.First().Cups[6]);

            Assert.Equal(
                new Cup(1355, "Copa Desafío Rubí VII División ", 7, 2, 2, 6, 0),
                result.LeagueList.First().Cups[7]);

            Assert.Equal(
                new Cup(1368, "Copa Desafío Zafiro VII División ", 7, 2, 3, 5, 0),
                result.LeagueList.First().Cups[8]);

            Assert.Equal(
                new Cup(1381, "Copa Consuelo VII División", 7, 3, 1, 6, 0),
                result.LeagueList.First().Cups[9]);

            Assert.Equal(3006, result.LeagueList.First().NationalTeamId);
            Assert.Equal(3047, result.LeagueList.First().U20TeamId);
            Assert.Equal(6751, result.LeagueList.First().ActiveTeams);
            Assert.Equal(6646, result.LeagueList.First().ActiveUsers);
            Assert.Equal(0, result.LeagueList.First().WaitingUsers);
            Assert.Equal(new DateTime(2025, 4, 4, 2, 0, 0), result.LeagueList.First().TrainingDate);
            Assert.Equal(new DateTime(2025, 4, 5, 3, 30, 0), result.LeagueList.First().EconomyDate);
            Assert.Equal(new DateTime(2025, 4, 2, 23, 10, 0), result.LeagueList.First().CupMatchDate);
            Assert.Equal(new DateTime(2025, 4, 6, 21, 40, 0), result.LeagueList.First().SeriesMatchDate);
            Assert.Equal(new DateTime(2025, 4, 7, 11, 0, 0), result.LeagueList.First().Sequence1);
            Assert.Equal(new DateTime(2025, 4, 8, 11, 0, 0), result.LeagueList.First().Sequence2);
            Assert.Equal(new DateTime(2025, 4, 1, 21, 40, 0), result.LeagueList.First().Sequence3);
            Assert.Equal(new DateTime(2025, 4, 3, 11, 0, 0), result.LeagueList.First().Sequence5);
            Assert.Equal(new DateTime(2025, 4, 4, 19, 20, 0), result.LeagueList.First().Sequence7);
            Assert.Equal(6, result.LeagueList.First().NumberOfLevels);
        }
    }
}