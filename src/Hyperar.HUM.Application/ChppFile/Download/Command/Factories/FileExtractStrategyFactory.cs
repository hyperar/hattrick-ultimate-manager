namespace Hyperar.HUM.Application.ChppFile.Download.Command.Factories
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Extract;
    using Hyperar.HUM.Shared.Enums;

    public class FileExtractStrategyFactory : IFileExtractStrategyFactory
    {
        private readonly CheckTokenExtractor checkTokenExtractor;

        private readonly ManagerCompendiumExtractor managerCompendiumExtractor;

        private readonly TeamDetailsExtractor teamDetailsExtractor;

        private readonly WorldDetailsExtractor worldDetailsExtractor;

        public FileExtractStrategyFactory(
            CheckTokenExtractor checkTokenExtractor,
            ManagerCompendiumExtractor managerCompendiumExtractor,
            TeamDetailsExtractor teamDetailsExtractor,
            WorldDetailsExtractor worldDetailsExtractor)
        {
            this.checkTokenExtractor = checkTokenExtractor;
            this.managerCompendiumExtractor = managerCompendiumExtractor;
            this.teamDetailsExtractor = teamDetailsExtractor;
            this.worldDetailsExtractor = worldDetailsExtractor;
        }

        public IFileExtractorStrategy GetFor(XmlFileType xmlFile)
        {
            return xmlFile switch
            {
                XmlFileType.CheckToken => this.checkTokenExtractor,
                XmlFileType.ManagerCompendium => this.managerCompendiumExtractor,
                XmlFileType.TeamDetails => this.teamDetailsExtractor,
                XmlFileType.WorldDetails => this.worldDetailsExtractor,
                _ => throw new ArgumentOutOfRangeException(nameof(xmlFile)),
            };
        }
    }
}