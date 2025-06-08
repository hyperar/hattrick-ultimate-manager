namespace Hyperar.HUM.Application.UnitTest.ChppFile.XmlFileParser
{
    using System;
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

        private const string ManagerCompendiumMultipleTeam = "Assets\\Xml\\ManagerCompendium\\ManagerCompendium_MultipleTeam.xml";

        private const string ManagerCompendiumNationalTeamAssistant = "Assets\\Xml\\ManagerCompendium\\ManagerCompendium_NationalTeamAssistant.xml";

        private const string ManagerCompendiumNationalTeamCoach = "Assets\\Xml\\ManagerCompendium\\ManagerCompendium_NationalTeamCoach.xml";

        private const string ManagerCompendiumNoAvatar = "Assets\\Xml\\ManagerCompendium\\ManagerCompendium_NoAvatar.xml";

        private const string ManagerCompendiumYouthLeague = "Assets\\Xml\\ManagerCompendium\\ManagerCompendium_YouthLeague.xml";

        private const string ManagerCompendiumYouthTeam = "Assets\\Xml\\ManagerCompendium\\ManagerCompendium_YouthTeam.xml";

        private readonly IXmlFileParser XmlFileParser;

        public ManagerCompendiumUnitTests(ServicesFixture fixture)
        {
            this.XmlFileParser = fixture.Services.GetRequiredService<IXmlFileParser>();
        }

        [Fact]
        public async Task ManagerCompendium_Avatar_ShouldBeEqual()
        {
            var fileContent = await OpenFile(ManagerCompendiumAvatar);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.XmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "managercompendium.xml",
                1.5m,
                12345678,
                new DateTime(2025, 3, 15, 12, 11, 5),
                new Manager(
                    12345678,
                    "%USER_NAME%",
                    "%SUPPOTER_PACKAGE%",
                    [
                        "%LOGIN_TIME_MASKED_IP_1%",
                        "%LOGIN_TIME_MASKED_IP_2%",
                        "%LOGIN_TIME_MASKED_IP_3%",
                        "%LOGIN_TIME_MASKED_IP_4%",
                        "%LOGIN_TIME_MASKED_IP_5%",
                        "%LOGIN_TIME_MASKED_IP_6%"
                    ],
                    new IdName(1, "%LANGUAGE_NAME_1%"),
                    new IdName(12, "%COUNTRY_NAME_12%"),
                    new Currency("€", 10),
                    [
                        new Team(
                            1234567,
                            "%TEAM_NAME_1234567%",
                            new IdName(1234567, "%ARENA_NAME_1234567%"),
                            new League(12, "%LEAGUE_NAME_12%", 78),
                            new IdName(12, "%COUNTRY_NAME_12%"),
                            new IdName(12345, "%SERIES_NAME_12345%"),
                            new IdName(123, "%REGION_NAME_123%"),
                            null)
                    ],
                    null,
                    null,
                    new Avatar(
                        "%FRAME_FILE_NAME%",
                        [
                            new Layer(9, 10, "%BACKGROUND_FILE_NAME%"),
                            new Layer(9, 10, "%BODY_FILE_NAME%"),
                            new Layer(9, 10, "%FACE_FILE_NAME%"),
                            new Layer(9, 10, "%BEARD_FILE_NAME%"),
                            new Layer(27, 26, "%EYES_FILE_NAME%"),
                            new Layer(9, 10, "%MOUTH_FILE_NAME%"),
                            new Layer(9, 10, "%NOSE_FILE_NAME%"),
                            new Layer(9, 10, "%HAIR_FILE_NAME%")
                        ])));

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ManagerCompendium_MultipleTeam_ShouldBeEqual()
        {
            var fileContent = await OpenFile(ManagerCompendiumMultipleTeam);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.XmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "managercompendium.xml",
                1.5m,
                12345678,
                new DateTime(2025, 3, 15, 12, 11, 5),
                new Manager(
                    12345678,
                    "%USER_NAME%",
                    "%SUPPOTER_PACKAGE%",
                    [
                        "%LOGIN_TIME_MASKED_IP_1%",
                        "%LOGIN_TIME_MASKED_IP_2%",
                        "%LOGIN_TIME_MASKED_IP_3%",
                        "%LOGIN_TIME_MASKED_IP_4%",
                        "%LOGIN_TIME_MASKED_IP_5%",
                        "%LOGIN_TIME_MASKED_IP_6%"
                    ],
                    new IdName(1, "%LANGUAGE_NAME_1%"),
                    new IdName(12, "%COUNTRY_NAME_12%"),
                    new Currency("€", 10),
                    [
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
                        new Team(
                            7654321,
                            "%TEAM_NAME_7654321%",
                            new IdName(1234567, "%ARENA_NAME_7654321%"),
                            new League(24, "%LEAGUE_NAME_24%", 54),
                            new IdName(24, "%COUNTRY_NAME_24%"),
                            new IdName(54321, "%SERIES_NAME_54321%"),
                            new IdName(678, "%REGION_NAME_678%"),
                            null)
                    ],
                    [
                        new IdName(1234, "%NATIONAL_TEAM_1234%"),
                        new IdName(2345, "%NATIONAL_TEAM_2345%")
                    ],
                    [
                        new IdName(3456, "%NATIONAL_TEAM_3456%"),
                        new IdName(4567, "%NATIONAL_TEAM_4567%")
                    ],
                    new Avatar(
                        "%FRAME_FILE_NAME%",
                        [
                            new Layer(9, 10, "%BACKGROUND_FILE_NAME%"),
                            new Layer(9, 10, "%BODY_FILE_NAME%"),
                            new Layer(9, 10, "%FACE_FILE_NAME%"),
                            new Layer(9, 10, "%BEARD_FILE_NAME%"),
                            new Layer(27, 26, "%EYES_FILE_NAME%"),
                            new Layer(9, 10, "%MOUTH_FILE_NAME%"),
                            new Layer(9, 10, "%NOSE_FILE_NAME%"),
                            new Layer(9, 10, "%HAIR_FILE_NAME%")
                        ])));

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ManagerCompendium_NationalTeamAssistant_ShouldBeEqual()
        {
            var fileContent = await OpenFile(ManagerCompendiumNationalTeamAssistant);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.XmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "managercompendium.xml",
                1.5m,
                12345678,
                new DateTime(2025, 3, 15, 12, 11, 5),
                new Manager(
                    12345678,
                    "%USER_NAME%",
                    "%SUPPOTER_PACKAGE%",
                    [
                        "%LOGIN_TIME_MASKED_IP_1%",
                        "%LOGIN_TIME_MASKED_IP_2%",
                        "%LOGIN_TIME_MASKED_IP_3%",
                        "%LOGIN_TIME_MASKED_IP_4%",
                        "%LOGIN_TIME_MASKED_IP_5%",
                        "%LOGIN_TIME_MASKED_IP_6%"
                    ],
                    new IdName(1, "%LANGUAGE_NAME_1%"),
                    new IdName(12, "%COUNTRY_NAME_12%"),
                    new Currency("€", 10),
                    [
                        new Team(
                            1234567,
                            "%TEAM_NAME_1234567%",
                            new IdName(1234567, "%ARENA_NAME_1234567%"),
                            new League(12, "%LEAGUE_NAME_12%", 78),
                            new IdName(12, "%COUNTRY_NAME_12%"),
                            new IdName(12345, "%SERIES_NAME_12345%"),
                            new IdName(123, "%REGION_NAME_123%"),
                            new YouthTeam(2345678, "%YOUTH_TEAM_2345678%", null))
                    ],
                    [
                        new IdName(1234, "%NATIONAL_TEAM_1234%"),
                        new IdName(2345, "%NATIONAL_TEAM_2345%")
                    ],
                    [
                        new IdName(3456, "%NATIONAL_TEAM_3456%"),
                        new IdName(4567, "%NATIONAL_TEAM_4567%")
                    ],
                    new Avatar(
                        "%FRAME_FILE_NAME%",
                        [
                            new Layer(9, 10, "%BACKGROUND_FILE_NAME%"),
                            new Layer(9, 10, "%BODY_FILE_NAME%"),
                            new Layer(9, 10, "%FACE_FILE_NAME%"),
                            new Layer(9, 10, "%BEARD_FILE_NAME%"),
                            new Layer(27, 26, "%EYES_FILE_NAME%"),
                            new Layer(9, 10, "%MOUTH_FILE_NAME%"),
                            new Layer(9, 10, "%NOSE_FILE_NAME%"),
                            new Layer(9, 10, "%HAIR_FILE_NAME%")
                        ])));

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ManagerCompendium_NationalTeamCoach_ShouldBeEqual()
        {
            var fileContent = await OpenFile(ManagerCompendiumNationalTeamCoach);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.XmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "managercompendium.xml",
                1.5m,
                12345678,
                new DateTime(2025, 3, 15, 12, 11, 5),
                new Manager(
                    12345678,
                    "%USER_NAME%",
                    "%SUPPOTER_PACKAGE%",
                    [
                        "%LOGIN_TIME_MASKED_IP_1%",
                        "%LOGIN_TIME_MASKED_IP_2%",
                        "%LOGIN_TIME_MASKED_IP_3%",
                        "%LOGIN_TIME_MASKED_IP_4%",
                        "%LOGIN_TIME_MASKED_IP_5%",
                        "%LOGIN_TIME_MASKED_IP_6%"
                    ],
                    new IdName(1, "%LANGUAGE_NAME_1%"),
                    new IdName(12, "%COUNTRY_NAME_12%"),
                    new Currency("€", 10),
                    [
                        new Team(
                            1234567,
                            "%TEAM_NAME_1234567%",
                            new IdName(1234567, "%ARENA_NAME_1234567%"),
                            new League(12, "%LEAGUE_NAME_12%", 78),
                            new IdName(12, "%COUNTRY_NAME_12%"),
                            new IdName(12345, "%SERIES_NAME_12345%"),
                            new IdName(123, "%REGION_NAME_123%"),
                            new YouthTeam(2345678, "%YOUTH_TEAM_2345678%", null))
                    ],
                    [
                        new IdName(1234, "%NATIONAL_TEAM_1234%"),
                        new IdName(2345, "%NATIONAL_TEAM_2345%")
                    ],
                    null,
                    new Avatar(
                        "%FRAME_FILE_NAME%",
                        [
                            new Layer(9, 10, "%BACKGROUND_FILE_NAME%"),
                            new Layer(9, 10, "%BODY_FILE_NAME%"),
                            new Layer(9, 10, "%FACE_FILE_NAME%"),
                            new Layer(9, 10, "%BEARD_FILE_NAME%"),
                            new Layer(27, 26, "%EYES_FILE_NAME%"),
                            new Layer(9, 10, "%MOUTH_FILE_NAME%"),
                            new Layer(9, 10, "%NOSE_FILE_NAME%"),
                            new Layer(9, 10, "%HAIR_FILE_NAME%")
                        ])));

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ManagerCompendium_NoAvatar_ShouldBeEqual()
        {
            var fileContent = await OpenFile(ManagerCompendiumNoAvatar);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.XmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "managercompendium.xml",
                1.5m,
                12345678,
                new DateTime(2025, 3, 15, 12, 11, 5),
                new Manager(
                    12345678,
                    "%USER_NAME%",
                    "%SUPPOTER_PACKAGE%",
                    [
                        "%LOGIN_TIME_MASKED_IP_1%",
                        "%LOGIN_TIME_MASKED_IP_2%",
                        "%LOGIN_TIME_MASKED_IP_3%",
                        "%LOGIN_TIME_MASKED_IP_4%",
                        "%LOGIN_TIME_MASKED_IP_5%",
                        "%LOGIN_TIME_MASKED_IP_6%"
                    ],
                    new IdName(1, "%LANGUAGE_NAME_1%"),
                    new IdName(12, "%COUNTRY_NAME_12%"),
                    new Currency("€", 10),
                    [
                        new Team(
                            1234567,
                            "%TEAM_NAME_1234567%",
                            new IdName(1234567, "%ARENA_NAME_1234567%"),
                            new League(12, "%LEAGUE_NAME_12%", 78),
                            new IdName(12, "%COUNTRY_NAME_12%"),
                            new IdName(12345, "%SERIES_NAME_12345%"),
                            new IdName(123, "%REGION_NAME_123%"),
                            null)
                    ],
                    null,
                    null,
                    null));

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ManagerCompendium_YouthLeague_ShouldBeEqual()
        {
            var fileContent = await OpenFile(ManagerCompendiumYouthLeague);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.XmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "managercompendium.xml",
                1.5m,
                12345678,
                new DateTime(2025, 3, 15, 12, 11, 5),
                new Manager(
                    12345678,
                    "%USER_NAME%",
                    "%SUPPOTER_PACKAGE%",
                    [
                        "%LOGIN_TIME_MASKED_IP_1%",
                        "%LOGIN_TIME_MASKED_IP_2%",
                        "%LOGIN_TIME_MASKED_IP_3%",
                        "%LOGIN_TIME_MASKED_IP_4%",
                        "%LOGIN_TIME_MASKED_IP_5%",
                        "%LOGIN_TIME_MASKED_IP_6%"
                    ],
                    new IdName(1, "%LANGUAGE_NAME_1%"),
                    new IdName(12, "%COUNTRY_NAME_12%"),
                    new Currency("€", 10),
                    [
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
                                    "%YOUTH_LEAGUE_234567%")))
                    ],
                    null,
                    null,
                    new Avatar(
                        "%FRAME_FILE_NAME%",
                        [
                            new Layer(9, 10, "%BACKGROUND_FILE_NAME%"),
                            new Layer(9, 10, "%BODY_FILE_NAME%"),
                            new Layer(9, 10, "%FACE_FILE_NAME%"),
                            new Layer(9, 10, "%BEARD_FILE_NAME%"),
                            new Layer(27, 26, "%EYES_FILE_NAME%"),
                            new Layer(9, 10, "%MOUTH_FILE_NAME%"),
                            new Layer(9, 10, "%NOSE_FILE_NAME%"),
                            new Layer(9, 10, "%HAIR_FILE_NAME%")
                        ])));

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task ManagerCompendium_YouthTeam_ShouldBeEqual()
        {
            var fileContent = await OpenFile(ManagerCompendiumYouthTeam);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.XmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "managercompendium.xml",
                1.5m,
                12345678,
                new DateTime(2025, 3, 15, 12, 11, 5),
                new Manager(
                    12345678,
                    "%USER_NAME%",
                    "%SUPPOTER_PACKAGE%",
                    [
                        "%LOGIN_TIME_MASKED_IP_1%",
                        "%LOGIN_TIME_MASKED_IP_2%",
                        "%LOGIN_TIME_MASKED_IP_3%",
                        "%LOGIN_TIME_MASKED_IP_4%",
                        "%LOGIN_TIME_MASKED_IP_5%",
                        "%LOGIN_TIME_MASKED_IP_6%"
                    ],
                    new IdName(1, "%LANGUAGE_NAME_1%"),
                    new IdName(12, "%COUNTRY_NAME_12%"),
                    new Currency("€", 10),
                    [
                        new Team(
                            1234567,
                            "%TEAM_NAME_1234567%",
                            new IdName(1234567, "%ARENA_NAME_1234567%"),
                            new League(12, "%LEAGUE_NAME_12%", 78),
                            new IdName(12, "%COUNTRY_NAME_12%"),
                            new IdName(12345, "%SERIES_NAME_12345%"),
                            new IdName(123, "%REGION_NAME_123%"),
                            new YouthTeam(2345678, "%YOUTH_TEAM_2345678%", null))
                    ],
                    null,
                    null,
                    new Avatar(
                        "%FRAME_FILE_NAME%",
                        [
                            new Layer(9, 10, "%BACKGROUND_FILE_NAME%"),
                            new Layer(9, 10, "%BODY_FILE_NAME%"),
                            new Layer(9, 10, "%FACE_FILE_NAME%"),
                            new Layer(9, 10, "%BEARD_FILE_NAME%"),
                            new Layer(27, 26, "%EYES_FILE_NAME%"),
                            new Layer(9, 10, "%MOUTH_FILE_NAME%"),
                            new Layer(9, 10, "%NOSE_FILE_NAME%"),
                            new Layer(9, 10, "%HAIR_FILE_NAME%")
                        ])));

            Assert.Equal(expected, result);
        }
    }
}