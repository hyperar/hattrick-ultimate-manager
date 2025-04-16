namespace Hyperar.HUM.Application.UnitTest.ChppFile.XmlFIleParser
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Shared.Models.Chpp.CheckToken;
    using Hyperar.HUM.Test.Shared;
    using Microsoft.Extensions.DependencyInjection;

    public class CheckTokenUnitTests : XmlFileParserBase, IClassFixture<ServicesFixture>
    {
        private const string CheckTokenAllScopes = "Assets\\Xml\\CheckToken\\CheckToken_AllScopes.xml";

        private const string CheckTokenManageChallengesScope = "Assets\\Xml\\CheckToken\\CheckToken_ManageChallengesScope.xml";

        private const string CheckTokenNoScopes = "Assets\\Xml\\CheckToken\\CheckToken_NoScopes.xml";

        private readonly IXmlFileParser xmlFileParser;

        public CheckTokenUnitTests(ServicesFixture fixture)
        {
            this.xmlFileParser = fixture.Services.GetRequiredService<IXmlFileParser>();
        }

        [Fact]
        public async Task CheckToken_AllScopes_ShouldBeEqual()
        {
            var fileContent = await OpenFile(CheckTokenAllScopes);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "check_token",
                1.0m,
                12345678,
                new DateTime(2025, 1, 1, 8, 15, 30),
                "%TOKEN_VALUE%",
                new DateTime(2025, 1, 1, 6, 0, 0),
                12345678,
                new DateTime(9999, 12, 31, 23, 59, 59),
                [
                    "manage_challenges",
                    "set_matchorder",
                    "manage_youthplayers",
                    "set_training",
                    "place_bid"
                ]);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task CheckToken_ManageChallengesScope_ShouldBeEqual()
        {
            var fileContent = await OpenFile(CheckTokenManageChallengesScope);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "check_token",
                1.0m,
                12345678,
                new DateTime(2025, 1, 1, 7, 15, 30),
                "%TOKEN_VALUE%",
                new DateTime(2025, 1, 1, 6, 0, 0),
                12345678,
                new DateTime(9999, 12, 31, 23, 59, 59),
                [
                    "manage_challenges"
                ]);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task CheckToken_NoScopes_ShouldBeEqual()
        {
            var fileContent = await OpenFile(CheckTokenNoScopes);

            Assert.NotNull(fileContent);

            Assert.NotEmpty(fileContent);

            var cancellationTokenSource = new CancellationTokenSource();

            var result = (HattrickData)await this.xmlFileParser.ParseXmlFileAsync(fileContent, cancellationTokenSource.Token);

            Assert.NotNull(result);

            Assert.IsType<HattrickData>(result);

            var expected = new HattrickData(
                "check_token",
                1.0m,
                12345678,
                new DateTime(2025, 1, 1, 6, 15, 30),
                "%TOKEN_VALUE%",
                new DateTime(2025, 1, 1, 6, 0, 0),
                12345678,
                new DateTime(9999, 12, 31, 23, 59, 59),
                Array.Empty<string>());

            Assert.Equal(expected, result);
        }
    }
}