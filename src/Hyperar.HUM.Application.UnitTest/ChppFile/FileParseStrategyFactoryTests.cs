namespace Hyperar.HUM.Application.UnitTest.ChppFile
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.Constants;
    using Hyperar.HUM.Test.Shared;
    using Microsoft.Extensions.DependencyInjection;

    public class FileParseStrategyFactoryTests : IClassFixture<ServicesFixture>
    {
        private readonly IFileParseStrategyFactory fileParseStrategyFactory;

        public FileParseStrategyFactoryTests(ServicesFixture fixture)
        {
            this.fileParseStrategyFactory = fixture.Services.GetRequiredService<IFileParseStrategyFactory>();
        }

        [Theory]
        [InlineData(FileName.CheckToken, typeof(CheckTokenParser))]
        [InlineData(FileName.Error, typeof(ErrorParser))]
        [InlineData(FileName.ManagerCompendium, typeof(ManagerCompendiumParser))]
        [InlineData(FileName.Players, typeof(PlayersParser))]
        [InlineData(FileName.TeamDetails, typeof(TeamDetailsParser))]
        [InlineData(FileName.WorldDetails, typeof(WorldDetailsParser))]
        public void FileParseStrategyFactoryImplementedXmlFileType_ShouldBeOfType(string fileName, Type returnType)
        {
            var strategy = this.fileParseStrategyFactory.GetFor(fileName);

            Assert.IsType(returnType, strategy);
        }

        [Theory]
        [InlineData(FileName.Achievements)]
        [InlineData(FileName.Alliances)]
        [InlineData(FileName.ArenaDetails)]
        [InlineData(FileName.Avatars)]
        [InlineData(FileName.Bookmarks)]
        [InlineData(FileName.Challenges)]
        [InlineData(FileName.Club)]
        [InlineData(FileName.CupMatches)]
        [InlineData(FileName.CurrentBids)]
        [InlineData(FileName.Economy)]
        [InlineData(FileName.Fans)]
        [InlineData(FileName.HallOfFamePlayers)]
        [InlineData(FileName.LadderDetails)]
        [InlineData(FileName.LadderList)]
        [InlineData(FileName.LeagueDetails)]
        [InlineData(FileName.LeagueFixtures)]
        [InlineData(FileName.LeagueLevels)]
        [InlineData(FileName.Live)]
        [InlineData(FileName.MatchDetails)]
        [InlineData(FileName.Matches)]
        [InlineData(FileName.MatchesArchive)]
        [InlineData(FileName.MatchLineUp)]
        [InlineData(FileName.MatchOrders)]
        [InlineData(FileName.NationalPlayers)]
        [InlineData(FileName.NationalTeamDetails)]
        [InlineData(FileName.NationalTeams)]
        [InlineData(FileName.PlayerDetails)]
        [InlineData(FileName.PlayerEvents)]
        [InlineData(FileName.RegionDetails)]
        [InlineData(FileName.Search)]
        [InlineData(FileName.StaffAvatars)]
        [InlineData(FileName.StaffList)]
        [InlineData(FileName.Supporters)]
        [InlineData(FileName.TournamentDetails)]
        [InlineData(FileName.TournamentFixtures)]
        [InlineData(FileName.TournamentLeagueTables)]
        [InlineData(FileName.TournamentList)]
        [InlineData(FileName.Training)]
        [InlineData(FileName.TrainingEvents)]
        [InlineData(FileName.TransferSearch)]
        [InlineData(FileName.TransfersPlayer)]
        [InlineData(FileName.TransfersTeam)]
        [InlineData(FileName.Translations)]
        [InlineData(FileName.WorldCup)]
        [InlineData(FileName.WorldLanguages)]
        [InlineData(FileName.YouthAvatars)]
        [InlineData(FileName.YouthLeagueDetails)]
        [InlineData(FileName.YouthLeagueFixtures)]
        [InlineData(FileName.YouthPlayerDetails)]
        [InlineData(FileName.YouthPlayerList)]
        [InlineData(FileName.YouthTeamDetails)]
        public void FileParseStrategyFactoryNotImplementedXmlFileType_ShouldThrowArgumentOutOfRangeException(string fileName)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => this.fileParseStrategyFactory.GetFor(fileName));
        }
    }
}