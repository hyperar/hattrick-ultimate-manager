namespace Hyperar.HUM.Application.ChppFile.Download.Command.Factories
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Extract;
    using Hyperar.HUM.Shared.Enums;

    public class FileExtractStrategyFactory : IFileExtractStrategyFactory
    {
        private readonly AvatarsExtractor avatarsExtractor;

        private readonly CheckTokenExtractor checkTokenExtractor;

        private readonly EmptyExtractor emptyExtractor;

        private readonly ManagerCompendiumExtractor managerCompendiumExtractor;

        private readonly PlayersExtractor playersExtractor;

        private readonly TeamDetailsExtractor teamDetailsExtractor;

        private readonly WorldDetailsExtractor worldDetailsExtractor;

        public FileExtractStrategyFactory(
            AvatarsExtractor avatarsExtractor,
            CheckTokenExtractor checkTokenExtractor,
            EmptyExtractor emptyExtractor,
            ManagerCompendiumExtractor managerCompendiumExtractor,
            PlayersExtractor playersExtractor,
            TeamDetailsExtractor teamDetailsExtractor,
            WorldDetailsExtractor worldDetailsExtractor)
        {
            this.avatarsExtractor = avatarsExtractor;
            this.checkTokenExtractor = checkTokenExtractor;
            this.emptyExtractor = emptyExtractor;
            this.managerCompendiumExtractor = managerCompendiumExtractor;
            this.playersExtractor = playersExtractor;
            this.teamDetailsExtractor = teamDetailsExtractor;
            this.worldDetailsExtractor = worldDetailsExtractor;
        }

        public IFileExtractorStrategy GetFor(XmlFileType xmlFile)
        {
            return xmlFile switch
            {
                XmlFileType.Avatars => this.avatarsExtractor,
                XmlFileType.CheckToken => this.checkTokenExtractor,
                XmlFileType.ManagerCompendium => this.managerCompendiumExtractor,
                XmlFileType.Players => this.playersExtractor,
                XmlFileType.TeamDetails => this.teamDetailsExtractor,
                XmlFileType.WorldDetails => this.worldDetailsExtractor,
                _ => throw new ArgumentOutOfRangeException(nameof(xmlFile)),
            };
        }
    }
}