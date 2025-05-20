namespace Hyperar.HUM.Application.UnitTest.ChppFile
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Test.Shared;
    using Microsoft.Extensions.DependencyInjection;

    public class FilePersisterStrategyFactoryTests : IClassFixture<ServicesFixture>
    {
        private readonly IFileDownloadTaskFactory fileDownloadTaskFactory;

        private readonly IFilePersisterStrategyFactory filePersisterStrategyFactory;

        public FilePersisterStrategyFactoryTests(ServicesFixture fixture)
        {
            this.filePersisterStrategyFactory = fixture.Services.GetRequiredService<IFilePersisterStrategyFactory>();
            this.fileDownloadTaskFactory = fixture.Services.GetRequiredService<IFileDownloadTaskFactory>();
        }

        [Theory]
        [InlineData(XmlFileType.ManagerCompendium, typeof(ManagerCompendiumPersister))]
        [InlineData(XmlFileType.TeamDetails, typeof(TeamDetailsPersister))]
        [InlineData(XmlFileType.WorldDetails, typeof(WorldDetailsPersister))]
        public void FilePersistStrategyFactoryImplementedXmlFileType_ShouldBeOfType(XmlFileType xmlFile, Type returnType)
        {
            var task = this.fileDownloadTaskFactory.BuildXmlFileDownloadTask(new Guid(), xmlFile, null);

            var strategy = this.filePersisterStrategyFactory.GetFor(task);

            Assert.IsType(returnType, strategy);
        }

        [Theory]
        [InlineData(XmlFileType.Achievements)]
        [InlineData(XmlFileType.Alliances)]
        [InlineData(XmlFileType.ArenaDetails)]
        [InlineData(XmlFileType.Avatars)]
        [InlineData(XmlFileType.Bookmarks)]
        [InlineData(XmlFileType.Challenges)]
        [InlineData(XmlFileType.Club)]
        [InlineData(XmlFileType.CupMatches)]
        [InlineData(XmlFileType.CurrentBids)]
        [InlineData(XmlFileType.Economy)]
        [InlineData(XmlFileType.Fans)]
        [InlineData(XmlFileType.HallOfFamePlayers)]
        [InlineData(XmlFileType.LadderDetails)]
        [InlineData(XmlFileType.LadderList)]
        [InlineData(XmlFileType.LeagueDetails)]
        [InlineData(XmlFileType.LeagueFixtures)]
        [InlineData(XmlFileType.LeagueLevels)]
        [InlineData(XmlFileType.Live)]
        [InlineData(XmlFileType.MatchDetails)]
        [InlineData(XmlFileType.Matches)]
        [InlineData(XmlFileType.MatchesArchive)]
        [InlineData(XmlFileType.MatchLineUp)]
        [InlineData(XmlFileType.MatchOrders)]
        [InlineData(XmlFileType.NationalPlayers)]
        [InlineData(XmlFileType.NationalTeamDetails)]
        [InlineData(XmlFileType.NationalTeams)]
        [InlineData(XmlFileType.PlayerDetails)]
        [InlineData(XmlFileType.PlayerEvents)]
        [InlineData(XmlFileType.Players)]
        [InlineData(XmlFileType.RegionDetails)]
        [InlineData(XmlFileType.Search)]
        [InlineData(XmlFileType.StaffAvatars)]
        [InlineData(XmlFileType.StaffList)]
        [InlineData(XmlFileType.Supporters)]
        [InlineData(XmlFileType.TournamentDetails)]
        [InlineData(XmlFileType.TournamentFixtures)]
        [InlineData(XmlFileType.TournamentLeagueTables)]
        [InlineData(XmlFileType.TournamentList)]
        [InlineData(XmlFileType.Training)]
        [InlineData(XmlFileType.TrainingEvents)]
        [InlineData(XmlFileType.TransferSearch)]
        [InlineData(XmlFileType.TransfersPlayer)]
        [InlineData(XmlFileType.TransfersTeam)]
        [InlineData(XmlFileType.Translations)]
        [InlineData(XmlFileType.WorldCup)]
        [InlineData(XmlFileType.WorldLanguages)]
        [InlineData(XmlFileType.YouthAvatars)]
        [InlineData(XmlFileType.YouthLeagueDetails)]
        [InlineData(XmlFileType.YouthLeagueFixtures)]
        [InlineData(XmlFileType.YouthPlayerDetails)]
        [InlineData(XmlFileType.YouthPlayerList)]
        [InlineData(XmlFileType.YouthTeamDetails)]
        public void FilePersistStrategyFactoryNotImplementedXmlFileType_ShouldBeEmptyPersister(XmlFileType xmlFile)
        {
            var task = this.fileDownloadTaskFactory.BuildXmlFileDownloadTask(new Guid(), xmlFile, null);

            var persister = this.filePersisterStrategyFactory.GetFor(task);

            Assert.IsType<EmptyPersister>(persister);
        }
    }
}