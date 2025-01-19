namespace Hyperar.HUM.ChppApiClient.UnitTest
{
    using System.Collections.Specialized;
    using System.Web;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Shared.Enums;
    using Microsoft.Extensions.DependencyInjection;

    public class ProtectedResourceUrlFactoryTests : IClassFixture<ServicesFixture>
    {
        private readonly IProtectedResourceUrlFactory protectedResourceUrlFactory;

        public ProtectedResourceUrlFactoryTests(ServicesFixture fixture)
        {
            this.protectedResourceUrlFactory = fixture.Services.GetRequiredService<IProtectedResourceUrlFactory>();
        }

        [Fact]
        public void ProtectedResourceUrlFactory_ShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.ArenaDetails,
                new NameValueCollection
                {
                    { "anotherValue", "value1" },
                }));
        }

        [Fact]
        public void ProtectedResourceUrlFactoryArenaDetails_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.ArenaDetails,
                new NameValueCollection
                {
                    { "statsType", "value1" },
                    { "arenaId", "value2" },
                    { "teamId", "value3" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("arenaDetails", query.Get("file"), true);
            Assert.Equal("1.7", query.Get("version"), true);
            Assert.Equal("value1", query.Get("statsType"), true);
            Assert.Equal("value2", query.Get("arenaId"), true);
            Assert.Equal("value3", query.Get("teamId"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryAvatars_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.Avatars,
                new NameValueCollection
                {
                    { "actionType", "value1" },
                    { "teamId", "value2" },
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("avatars", query.Get("file"), true);
            Assert.Equal("1.1", query.Get("version"), true);
            Assert.Equal("value1", query.Get("actionType"), true);
            Assert.Equal("value2", query.Get("teamId"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryClub_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.Club,
                new NameValueCollection
                {
                    { "teamId", "value1" },
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("club", query.Get("file"), true);
            Assert.Equal("1.5", query.Get("version"), true);
            Assert.Equal("value1", query.Get("teamId"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryHallOfFamePlayers_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.HallOfFamePlayers,
                new NameValueCollection
                {
                    { "teamId", "value1" },
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("hofplayers", query.Get("file"), true);
            Assert.Equal("1.2", query.Get("version"), true);
            Assert.Equal("value1", query.Get("teamId"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryLeagueDetails_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.LeagueDetails,
                new NameValueCollection
                {
                    { "leagueLevelUnitId", "value1" },
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("leaguedetails", query.Get("file"), true);
            Assert.Equal("1.6", query.Get("version"), true);
            Assert.Equal("value1", query.Get("leagueLevelUnitId"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryManagerCompendium_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.ManagerCompendium,
                new NameValueCollection
                {
                    { "userId", "value1" },
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("managercompendium", query.Get("file"), true);
            Assert.Equal("1.5", query.Get("version"), true);
            Assert.Equal("value1", query.Get("userId"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryMatchArchive_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.MatchArchive,
                new NameValueCollection
                {
                    { "teamId", "value1" },
                    { "isYouth", "value2" },
                    { "firstMatchDate", "value3" },
                    { "lastMatchDate", "value4" },
                    { "season", "value5" },
                    { "includeHTO", "value6" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("matchesarchive", query.Get("file"), true);
            Assert.Equal("1.5", query.Get("version"), true);
            Assert.Equal("value1", query.Get("teamId"), true);
            Assert.Equal("value2", query.Get("isYouth"), true);
            Assert.Equal("value3", query.Get("firstMatchDate"), true);
            Assert.Equal("value4", query.Get("lastMatchDate"), true);
            Assert.Equal("value5", query.Get("season"), true);
            Assert.Equal("value6", query.Get("includeHTO"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryMatchDetails_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.MatchDetails,
                new NameValueCollection
                {
                    { "matchEvents", "value1" },
                    { "matchId", "value2" },
                    { "sourceSystem", "value3" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("matchdetails", query.Get("file"), true);
            Assert.Equal("3.1", query.Get("version"), true);
            Assert.Equal("value1", query.Get("matchEvents"), true);
            Assert.Equal("value2", query.Get("matchId"), true);
            Assert.Equal("value3", query.Get("sourceSystem"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryMatches_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.Matches,
                new NameValueCollection
                {
                    { "teamId", "value1" },
                    { "isYouth", "value2" },
                    { "lastMatchDate", "value3" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("matches", query.Get("file"), true);
            Assert.Equal("2.9", query.Get("version"), true);
            Assert.Equal("value1", query.Get("teamId"), true);
            Assert.Equal("value2", query.Get("isYouth"), true);
            Assert.Equal("value3", query.Get("lastMatchDate"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryMatchLineUp_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.MatchLineUp,
                new NameValueCollection
                {
                    { "sourceSystem", "value1" },
                    { "matchId", "value2" },
                    { "teamId", "value3" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("matchlineup", query.Get("file"), true);
            Assert.Equal("2.1", query.Get("version"), true);
            Assert.Equal("value1", query.Get("sourceSystem"), true);
            Assert.Equal("value2", query.Get("matchId"), true);
            Assert.Equal("value3", query.Get("teamId"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryPlayerDetails_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.PlayerDetails,
                new NameValueCollection
                {
                    { "actionType", "value1" },
                    { "playerId", "value2" },
                    { "includeMatchInfo", "value3" },
                    { "teamId", "value4" },
                    { "bidAmount", "value5" },
                    { "maxBidAmount", "value6" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("playerdetails", query.Get("file"), true);
            Assert.Equal("3.0", query.Get("version"), true);
            Assert.Equal("value1", query.Get("actionType"), true);
            Assert.Equal("value2", query.Get("playerId"), true);
            Assert.Equal("value3", query.Get("includeMatchInfo"), true);
            Assert.Equal("value4", query.Get("teamId"), true);
            Assert.Equal("value5", query.Get("bidAmount"), true);
            Assert.Equal("value6", query.Get("maxBidAmount"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryPlayers_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.Players,
                new NameValueCollection
                {
                    { "actionType", "value1" },
                    { "orderBy", "value2" },
                    { "teamId", "value3" },
                    { "includeMatchInfo", "value4" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("players", query.Get("file"), true);
            Assert.Equal("2.5", query.Get("version"), true);
            Assert.Equal("value1", query.Get("actionType"), true);
            Assert.Equal("value2", query.Get("orderBy"), true);
            Assert.Equal("value3", query.Get("teamId"), true);
            Assert.Equal("value4", query.Get("includeMatchInfo"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryStaffAvatars_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.StaffAvatars,
                new NameValueCollection
                {
                    { "teamId", "value1" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("staffavatars", query.Get("file"), true);
            Assert.Equal("1.1", query.Get("version"), true);
            Assert.Equal("value1", query.Get("teamId"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryStaffList_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.StaffList,
                new NameValueCollection
                {
                    { "teamId", "value1" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("stafflist", query.Get("file"), true);
            Assert.Equal("1.2", query.Get("version"), true);
            Assert.Equal("value1", query.Get("teamId"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryTeamDetails_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.TeamDetails,
                new NameValueCollection
                {
                    { "teamId", "value1" },
                    { "userId", "value2" },
                    { "includeDomesticFlags", "value3" },
                    { "includeFlags", "value4" },
                    { "includeSupporters", "value5" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("teamdetails", query.Get("file"), true);
            Assert.Equal("3.6", query.Get("version"), true);
            Assert.Equal("value1", query.Get("teamId"), true);
            Assert.Equal("value2", query.Get("userId"), true);
            Assert.Equal("value3", query.Get("includeDomesticFlags"), true);
            Assert.Equal("value4", query.Get("includeFlags"), true);
            Assert.Equal("value5", query.Get("includeSupporters"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryWorldDetails_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.WorldDetails,
                new NameValueCollection
                {
                    { "countryId", "value1" },
                    { "includeRegions", "value2" },
                    { "leagueId", "value3" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("worlddetails", query.Get("file"), true);
            Assert.Equal("1.9", query.Get("version"), true);
            Assert.Equal("value1", query.Get("countryId"), true);
            Assert.Equal("value2", query.Get("includeRegions"), true);
            Assert.Equal("value3", query.Get("leagueId"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryYouthAvatars_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.YouthAvatars,
                new NameValueCollection
                {
                    { "youthTeamId", "value1" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("youthavatars", query.Get("file"), true);
            Assert.Equal("1.2", query.Get("version"), true);
            Assert.Equal("value1", query.Get("youthTeamId"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryYouthLeagueDetails_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.YouthLeagueDetails,
                new NameValueCollection
                {
                    { "youthLeagueId", "value1" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("youthleaguedetails", query.Get("file"), true);
            Assert.Equal("1.0", query.Get("version"), true);
            Assert.Equal("value1", query.Get("youthLeagueId"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryYouthPlayerDetails_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.YouthPlayerDetails,
                new NameValueCollection
                {
                    { "actionType", "value1" },
                    { "youthPlayerId", "value2" },
                    { "showScoutCall", "value3" },
                    { "showLastMatch", "value4" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("youthplayerdetails", query.Get("file"), true);
            Assert.Equal("1.2", query.Get("version"), true);
            Assert.Equal("value1", query.Get("actionType"), true);
            Assert.Equal("value2", query.Get("youthPlayerId"), true);
            Assert.Equal("value3", query.Get("showScoutCall"), true);
            Assert.Equal("value4", query.Get("showLastMatch"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryYouthPlayerList_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.YouthPlayerList,
                new NameValueCollection
                {
                    { "actionType", "value1" },
                    { "orderBy", "value2" },
                    { "youthTeamId", "value3" },
                    { "showScoutCall", "value4" },
                    { "showLastMatch", "value5" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("youthplayerlist", query.Get("file"), true);
            Assert.Equal("1.2", query.Get("version"), true);
            Assert.Equal("value1", query.Get("actionType"), true);
            Assert.Equal("value2", query.Get("orderBy"), true);
            Assert.Equal("value3", query.Get("youthTeamId"), true);
            Assert.Equal("value4", query.Get("showScoutCall"), true);
            Assert.Equal("value5", query.Get("showLastMatch"), true);
        }

        [Fact]
        public void ProtectedResourceUrlFactoryYouthTeamDetails_ShouldBeEqual()
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(
                XmlFileType.YouthTeamDetails,
                new NameValueCollection
                {
                    { "youthTeamId", "value1" },
                    { "showScouts", "value2" }
                });

            var uri = new Uri(url);

            var query = HttpUtility.ParseQueryString(uri.Query);

            Assert.Equal("youthteamdetails", query.Get("file"), true);
            Assert.Equal("1.2", query.Get("version"), true);
            Assert.Equal("value1", query.Get("youthTeamId"), true);
            Assert.Equal("value2", query.Get("showScouts"), true);
        }
    }
}