namespace Hyperar.HUM.Application.UnitTest.ChppFile.XmlFIleParser
{
    using System;
    using System.IO;
    using System.Reflection;
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

            Assert.Equal("check_token", result.FileName);
            Assert.Equal(1.0m, result.Version);
            Assert.Equal(12345678, result.UserId);
            Assert.Equal(new DateTime(2025, 1, 1, 8, 15, 30), result.FetchedDate);
            Assert.Equal("%TOKEN_VALUE%", result.Token);
            Assert.Equal(new DateTime(2025, 1, 1, 6, 0, 0), result.Created);
            Assert.Equal(12345678, result.User);
            Assert.Equal(new DateTime(9999, 12, 31, 23, 59, 59), result.Expires);
            Assert.Equal(
                new string[]
                {
                    "manage_challenges",
                    "set_matchorder",
                    "manage_youthplayers",
                    "set_training",
                    "place_bid"
                },
                result.ExtendedPermissions);
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

            Assert.Equal("check_token", result.FileName);
            Assert.Equal(1.0m, result.Version);
            Assert.Equal(12345678, result.UserId);
            Assert.Equal(new DateTime(2025, 1, 1, 7, 15, 30), result.FetchedDate);
            Assert.Equal("%TOKEN_VALUE%", result.Token);
            Assert.Equal(new DateTime(2025, 1, 1, 6, 0, 0), result.Created);
            Assert.Equal(12345678, result.User);
            Assert.Equal(new DateTime(9999, 12, 31, 23, 59, 59), result.Expires);
            Assert.Equal(
                new string[]
                {
                    "manage_challenges"
                },
                result.ExtendedPermissions);
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

            Assert.Equal("check_token", result.FileName);
            Assert.Equal(1.0m, result.Version);
            Assert.Equal(12345678, result.UserId);
            Assert.Equal(new DateTime(2025, 1, 1, 6, 15, 30), result.FetchedDate);
            Assert.Equal("%TOKEN_VALUE%", result.Token);
            Assert.Equal(new DateTime(2025, 1, 1, 6, 0, 0), result.Created);
            Assert.Equal(12345678, result.User);
            Assert.Equal(new DateTime(9999, 12, 31, 23, 59, 59), result.Expires);
            Assert.Equal(Array.Empty<string>(), result.ExtendedPermissions);
        }
    }
}