namespace Hyperar.HUM.Application.UnitTest.ChppFile.XmlFIleParser
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Shared.Models.Chpp;
    using Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium;
    using Hyperar.HUM.Test.Shared;
    using Microsoft.Extensions.DependencyInjection;

    public class ManagerCompendiumUnitTests : XmlFileParserBase, IClassFixture<ServicesFixture>
    {
        private const string ManagerCompendiumAvatar = "Assets\\Xml\\ManagerCompendium\\ManagerCompendium_Avatar.xml";

        private const string ManagerCompendiumNationalTeamAssistant = "Assets\\Xml\\ManagerCompendium\\ManagerCompendium_NationalTeamAssistant.xml";

        private const string ManagerCompendiumNationalTeamCoach = "Assets\\Xml\\ManagerCompendium\\ManagerCompendium_NationalTeamCoach.xml";

        private const string ManagerCompendiumNoAvatar = "Assets\\Xml\\ManagerCompendium\\ManagerCompendium_NoAvatar.xml";

        private const string ManagerCompendiumYouthLeague = "Assets\\Xml\\ManagerCompendium\\ManagerCompendium_YouthLeague.xml";

        private const string ManagerCompendiumYouthTeam = "Assets\\Xml\\ManagerCompendium\\ManagerCompendium_YouthTeam.xml";

        private readonly IXmlFileParser xmlFileParser;

        public ManagerCompendiumUnitTests(ServicesFixture fixture)
        {
            this.xmlFileParser = fixture.Services.GetRequiredService<IXmlFileParser>();
        }

        [Fact]
        public async Task ManagerCompendium_Avatar_ShouldBeEqual()
        {
            var fileContent = await OpenFile(ManagerCompendiumAvatar);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            Assert.Equal("managercompendium.xml", result.FileName);
            Assert.Equal(1.5m, result.Version);
            Assert.Equal(12345678, result.UserId);
            Assert.Equal(new DateTime(2025, 3, 15, 12, 11, 5), result.FetchedDate);
            Assert.Equal(12345678, result.Manager.UserId);
            Assert.Equal("%USER_NAME%", result.Manager.LoginName);
            Assert.Equal("%SUPPOTER_PACKAGE%", result.Manager.SupporterTier);

            Assert.Equal(
                new string[]
                {
                    "%LOGIN_TIME_MASKED_IP_1%",
                    "%LOGIN_TIME_MASKED_IP_2%",
                    "%LOGIN_TIME_MASKED_IP_3%",
                    "%LOGIN_TIME_MASKED_IP_4%",
                    "%LOGIN_TIME_MASKED_IP_5%",
                    "%LOGIN_TIME_MASKED_IP_6%",
                },
                result.Manager.LastLogins);

            Assert.Equal(
                new IdName(1, "%LANGUAGE_NAME_1%"),
                result.Manager.Language);

            Assert.Equal(
                new IdName(12, "%COUNTRY_NAME_12%"),
                result.Manager.Country);

            Assert.Equal(
                new Currency("€", 10),
                result.Manager.Currency);

            Assert.Equal(
                new Team(
                    1234567,
                    "%TEAM_NAME_1234567%",
                    new IdName(1234567, "%ARENA_NAME_1234567%"),
                    new League(12, "%LEAGUE_NAME_12%", 78),
                    new IdName(12, "%COUNTRY_NAME_12%"),
                    new IdName(12345, "%SERIES_NAME_12345%"),
                    new IdName(123, "%REGION_NAME_123%"),
                    null),
                result.Manager.Teams.First());

            Assert.Null(result.Manager.NationalTeamCoach);
            Assert.Null(result.Manager.NationalTeamAssistant);
            Assert.NotNull(result.Manager.Avatar);

            Assert.Equal(
                "%FRAME_FILE_NAME%",
                result.Manager.Avatar.BackgroundImage);

            Assert.NotNull(result.Manager.Avatar.Layers);

            Assert.Equal(
                new Layer(9, 10, "%BACKGROUND_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(0));

            Assert.Equal(
                new Layer(9, 10, "%BODY_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(1));

            Assert.Equal(
                new Layer(9, 10, "%FACE_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(2));

            Assert.Equal(
                new Layer(9, 10, "%BEARD_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(3));

            Assert.Equal(
                new Layer(27, 26, "%EYES_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(4));

            Assert.Equal(
                new Layer(9, 10, "%MOUTH_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(5));

            Assert.Equal(
                new Layer(9, 10, "%NOSE_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(6));

            Assert.Equal(
                new Layer(9, 10, "%HAIR_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(7));
        }

        [Fact]
        public async Task ManagerCompendium_NationalTeamAssistant_ShouldBeEqual()

        {
            var fileContent = await OpenFile(ManagerCompendiumNationalTeamAssistant);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            Assert.Equal("managercompendium.xml", result.FileName);
            Assert.Equal(1.5m, result.Version);
            Assert.Equal(12345678, result.UserId);
            Assert.Equal(new DateTime(2025, 3, 15, 12, 11, 5), result.FetchedDate);
            Assert.Equal(12345678, result.Manager.UserId);
            Assert.Equal("%USER_NAME%", result.Manager.LoginName);
            Assert.Equal("%SUPPOTER_PACKAGE%", result.Manager.SupporterTier);

            Assert.Equal(
                new string[]
                {
                    "%LOGIN_TIME_MASKED_IP_1%",
                    "%LOGIN_TIME_MASKED_IP_2%",
                    "%LOGIN_TIME_MASKED_IP_3%",
                    "%LOGIN_TIME_MASKED_IP_4%",
                    "%LOGIN_TIME_MASKED_IP_5%",
                    "%LOGIN_TIME_MASKED_IP_6%",
                },
                result.Manager.LastLogins);

            Assert.Equal(
                new IdName(1, "%LANGUAGE_NAME_1%"),
                result.Manager.Language);

            Assert.Equal(
                new IdName(12, "%COUNTRY_NAME_12%"),
                result.Manager.Country);

            Assert.Equal(
                new Currency("€", 10),
                result.Manager.Currency);

            Assert.Equal(
                new Team(
                    1234567,
                    "%TEAM_NAME_1234567%",
                    new IdName(1234567, "%ARENA_NAME_1234567%"),
                    new League(12, "%LEAGUE_NAME_12%", 78),
                    new IdName(12, "%COUNTRY_NAME_12%"),
                    new IdName(12345, "%SERIES_NAME_12345%"),
                    new IdName(123, "%REGION_NAME_123%"),
                    new YouthTeam(2345678, "%YOUTH_TEAM_2345678%", null)),
                result.Manager.Teams.First());

            Assert.NotNull(result.Manager.NationalTeamCoach);

            Assert.Equal(
                new IdName[]
                {
                    new IdName(1234, "%NATIONAL_TEAM_1234%"),
                    new IdName(2345, "%NATIONAL_TEAM_2345%")
                },
                result.Manager.NationalTeamCoach);

            Assert.NotNull(result.Manager.NationalTeamAssistant);

            Assert.Equal(
                new IdName[]
                {
                    new IdName(3456, "%NATIONAL_TEAM_3456%"),
                    new IdName(4567, "%NATIONAL_TEAM_4567%")
                },
                result.Manager.NationalTeamAssistant);

            Assert.NotNull(result.Manager.Avatar);

            Assert.Equal(
                "%FRAME_FILE_NAME%",
                result.Manager.Avatar.BackgroundImage);

            Assert.NotNull(result.Manager.Avatar.Layers);

            Assert.Equal(
                new Layer(9, 10, "%BACKGROUND_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(0));

            Assert.Equal(
                new Layer(9, 10, "%BODY_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(1));

            Assert.Equal(
                new Layer(9, 10, "%FACE_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(2));

            Assert.Equal(
                new Layer(9, 10, "%BEARD_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(3));

            Assert.Equal(
                new Layer(27, 26, "%EYES_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(4));

            Assert.Equal(
                new Layer(9, 10, "%MOUTH_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(5));

            Assert.Equal(
                new Layer(9, 10, "%NOSE_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(6));

            Assert.Equal(
                new Layer(9, 10, "%HAIR_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(7));
        }

        [Fact]
        public async Task ManagerCompendium_NationalTeamCoach_ShouldBeEqual()
        {
            var fileContent = await OpenFile(ManagerCompendiumNationalTeamCoach);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            Assert.Equal("managercompendium.xml", result.FileName);
            Assert.Equal(1.5m, result.Version);
            Assert.Equal(12345678, result.UserId);
            Assert.Equal(new DateTime(2025, 3, 15, 12, 11, 5), result.FetchedDate);
            Assert.Equal(12345678, result.Manager.UserId);
            Assert.Equal("%USER_NAME%", result.Manager.LoginName);
            Assert.Equal("%SUPPOTER_PACKAGE%", result.Manager.SupporterTier);

            Assert.Equal(
                new string[]
                {
                    "%LOGIN_TIME_MASKED_IP_1%",
                    "%LOGIN_TIME_MASKED_IP_2%",
                    "%LOGIN_TIME_MASKED_IP_3%",
                    "%LOGIN_TIME_MASKED_IP_4%",
                    "%LOGIN_TIME_MASKED_IP_5%",
                    "%LOGIN_TIME_MASKED_IP_6%",
                },
                result.Manager.LastLogins);

            Assert.Equal(
                new IdName(1, "%LANGUAGE_NAME_1%"),
                result.Manager.Language);

            Assert.Equal(
                new IdName(12, "%COUNTRY_NAME_12%"),
                result.Manager.Country);

            Assert.Equal(
                new Currency("€", 10),
                result.Manager.Currency);

            Assert.Equal(
                new Team(
                    1234567,
                    "%TEAM_NAME_1234567%",
                    new IdName(1234567, "%ARENA_NAME_1234567%"),
                    new League(12, "%LEAGUE_NAME_12%", 78),
                    new IdName(12, "%COUNTRY_NAME_12%"),
                    new IdName(12345, "%SERIES_NAME_12345%"),
                    new IdName(123, "%REGION_NAME_123%"),
                    new YouthTeam(2345678, "%YOUTH_TEAM_2345678%", null)),
                result.Manager.Teams.First());

            Assert.NotNull(result.Manager.NationalTeamCoach);

            Assert.Equal(
                new IdName[]
                {
                    new IdName(1234, "%NATIONAL_TEAM_1234%"),
                    new IdName(2345, "%NATIONAL_TEAM_2345%")
                },
                result.Manager.NationalTeamCoach);

            Assert.Null(result.Manager.NationalTeamAssistant);
            Assert.NotNull(result.Manager.Avatar);

            Assert.Equal(
                "%FRAME_FILE_NAME%",
                result.Manager.Avatar.BackgroundImage);

            Assert.NotNull(result.Manager.Avatar.Layers);

            Assert.Equal(
                new Layer(9, 10, "%BACKGROUND_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(0));

            Assert.Equal(
                new Layer(9, 10, "%BODY_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(1));

            Assert.Equal(
                new Layer(9, 10, "%FACE_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(2));

            Assert.Equal(
                new Layer(9, 10, "%BEARD_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(3));

            Assert.Equal(
                new Layer(27, 26, "%EYES_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(4));

            Assert.Equal(
                new Layer(9, 10, "%MOUTH_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(5));

            Assert.Equal(
                new Layer(9, 10, "%NOSE_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(6));

            Assert.Equal(
                new Layer(9, 10, "%HAIR_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(7));
        }

        [Fact]
        public async Task ManagerCompendium_NoAvatar_ShouldBeEqual()
        {
            var fileContent = await OpenFile(ManagerCompendiumNoAvatar);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            Assert.Equal("managercompendium.xml", result.FileName);
            Assert.Equal(1.5m, result.Version);
            Assert.Equal(12345678, result.UserId);
            Assert.Equal(new DateTime(2025, 3, 15, 12, 11, 5), result.FetchedDate);
            Assert.Equal(12345678, result.Manager.UserId);
            Assert.Equal("%USER_NAME%", result.Manager.LoginName);
            Assert.Equal("%SUPPOTER_PACKAGE%", result.Manager.SupporterTier);

            Assert.Equal(
                new string[]
                {
                    "%LOGIN_TIME_MASKED_IP_1%",
                    "%LOGIN_TIME_MASKED_IP_2%",
                    "%LOGIN_TIME_MASKED_IP_3%",
                    "%LOGIN_TIME_MASKED_IP_4%",
                    "%LOGIN_TIME_MASKED_IP_5%",
                    "%LOGIN_TIME_MASKED_IP_6%",
                },
                result.Manager.LastLogins);

            Assert.Equal(
                new IdName(1, "%LANGUAGE_NAME_1%"),
                result.Manager.Language);

            Assert.Equal(
                new IdName(12, "%COUNTRY_NAME_12%"),
                result.Manager.Country);

            Assert.Equal(
                new Currency("€", 10),
                result.Manager.Currency);

            Assert.Equal(
                new Team(
                    1234567,
                    "%TEAM_NAME_1234567%",
                    new IdName(1234567, "%ARENA_NAME_1234567%"),
                    new League(12, "%LEAGUE_NAME_12%", 78),
                    new IdName(12, "%COUNTRY_NAME_12%"),
                    new IdName(12345, "%SERIES_NAME_12345%"),
                    new IdName(123, "%REGION_NAME_123%"),
                    null),
                result.Manager.Teams.First());

            Assert.Null(result.Manager.NationalTeamCoach);
            Assert.Null(result.Manager.NationalTeamAssistant);
            Assert.Null(result.Manager.Avatar);
        }

        [Fact]
        public async Task ManagerCompendium_YouthLeague_ShouldBeEqual()
        {
            var fileContent = await OpenFile(ManagerCompendiumYouthLeague);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            Assert.Equal("managercompendium.xml", result.FileName);
            Assert.Equal(1.5m, result.Version);
            Assert.Equal(12345678, result.UserId);
            Assert.Equal(new DateTime(2025, 3, 15, 12, 11, 5), result.FetchedDate);
            Assert.Equal(12345678, result.Manager.UserId);
            Assert.Equal("%USER_NAME%", result.Manager.LoginName);
            Assert.Equal("%SUPPOTER_PACKAGE%", result.Manager.SupporterTier);

            Assert.Equal(
                new string[]
                {
                    "%LOGIN_TIME_MASKED_IP_1%",
                    "%LOGIN_TIME_MASKED_IP_2%",
                    "%LOGIN_TIME_MASKED_IP_3%",
                    "%LOGIN_TIME_MASKED_IP_4%",
                    "%LOGIN_TIME_MASKED_IP_5%",
                    "%LOGIN_TIME_MASKED_IP_6%",
                },
                result.Manager.LastLogins);

            Assert.Equal(
                new IdName(1, "%LANGUAGE_NAME_1%"),
                result.Manager.Language);

            Assert.Equal(
                new IdName(12, "%COUNTRY_NAME_12%"),
                result.Manager.Country);

            Assert.Equal(
                new Currency("€", 10),
                result.Manager.Currency);

            Assert.Equal(
                new Team(
                    1234567,
                    "%TEAM_NAME_1234567%",
                    new IdName(1234567, "%ARENA_NAME_1234567%"),
                    new League(12, "%LEAGUE_NAME_12%", 78),
                    new IdName(12, "%COUNTRY_NAME_12%"),
                    new IdName(12345, "%SERIES_NAME_12345%"),
                    new IdName(123, "%REGION_NAME_123%"),
                    new YouthTeam(
                        2345678,
                        "%YOUTH_TEAM_2345678%",
                        new IdName(
                            234567,
                            "%YOUTH_LEAGUE_234567%"))),
                result.Manager.Teams.First());

            Assert.Null(result.Manager.NationalTeamCoach);
            Assert.Null(result.Manager.NationalTeamAssistant);
            Assert.NotNull(result.Manager.Avatar);

            Assert.Equal(
                "%FRAME_FILE_NAME%",
                result.Manager.Avatar.BackgroundImage);

            Assert.NotNull(result.Manager.Avatar.Layers);

            Assert.Equal(
                new Layer(9, 10, "%BACKGROUND_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(0));

            Assert.Equal(
                new Layer(9, 10, "%BODY_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(1));

            Assert.Equal(
                new Layer(9, 10, "%FACE_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(2));

            Assert.Equal(
                new Layer(9, 10, "%BEARD_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(3));

            Assert.Equal(
                new Layer(27, 26, "%EYES_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(4));

            Assert.Equal(
                new Layer(9, 10, "%MOUTH_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(5));

            Assert.Equal(
                new Layer(9, 10, "%NOSE_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(6));

            Assert.Equal(
                new Layer(9, 10, "%HAIR_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(7));
        }

        [Fact]
        public async Task ManagerCompendium_YouthTeam_ShouldBeEqual()
        {
            var fileContent = await OpenFile(ManagerCompendiumYouthTeam);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            Assert.Equal("managercompendium.xml", result.FileName);
            Assert.Equal(1.5m, result.Version);
            Assert.Equal(12345678, result.UserId);
            Assert.Equal(new DateTime(2025, 3, 15, 12, 11, 5), result.FetchedDate);
            Assert.Equal(12345678, result.Manager.UserId);
            Assert.Equal("%USER_NAME%", result.Manager.LoginName);
            Assert.Equal("%SUPPOTER_PACKAGE%", result.Manager.SupporterTier);

            Assert.Equal(
                new string[]
                {
                    "%LOGIN_TIME_MASKED_IP_1%",
                    "%LOGIN_TIME_MASKED_IP_2%",
                    "%LOGIN_TIME_MASKED_IP_3%",
                    "%LOGIN_TIME_MASKED_IP_4%",
                    "%LOGIN_TIME_MASKED_IP_5%",
                    "%LOGIN_TIME_MASKED_IP_6%",
                },
                result.Manager.LastLogins);

            Assert.Equal(
                new IdName(1, "%LANGUAGE_NAME_1%"),
                result.Manager.Language);

            Assert.Equal(
                new IdName(12, "%COUNTRY_NAME_12%"),
                result.Manager.Country);

            Assert.Equal(
                new Currency("€", 10),
                result.Manager.Currency);

            Assert.Equal(
                new Team(
                    1234567,
                    "%TEAM_NAME_1234567%",
                    new IdName(1234567, "%ARENA_NAME_1234567%"),
                    new League(12, "%LEAGUE_NAME_12%", 78),
                    new IdName(12, "%COUNTRY_NAME_12%"),
                    new IdName(12345, "%SERIES_NAME_12345%"),
                    new IdName(123, "%REGION_NAME_123%"),
                    new YouthTeam(2345678, "%YOUTH_TEAM_2345678%", null)),
                result.Manager.Teams.First());

            Assert.Null(result.Manager.NationalTeamCoach);
            Assert.Null(result.Manager.NationalTeamAssistant);
            Assert.NotNull(result.Manager.Avatar);

            Assert.Equal(
                "%FRAME_FILE_NAME%",
                result.Manager.Avatar.BackgroundImage);

            Assert.NotNull(result.Manager.Avatar.Layers);

            Assert.Equal(
                new Layer(9, 10, "%BACKGROUND_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(0));

            Assert.Equal(
                new Layer(9, 10, "%BODY_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(1));

            Assert.Equal(
                new Layer(9, 10, "%FACE_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(2));

            Assert.Equal(
                new Layer(9, 10, "%BEARD_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(3));

            Assert.Equal(
                new Layer(27, 26, "%EYES_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(4));

            Assert.Equal(
                new Layer(9, 10, "%MOUTH_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(5));

            Assert.Equal(
                new Layer(9, 10, "%NOSE_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(6));

            Assert.Equal(
                new Layer(9, 10, "%HAIR_FILE_NAME%"),
                result.Manager.Avatar.Layers.ElementAt(7));
        }
    }
}