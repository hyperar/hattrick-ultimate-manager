namespace Hyperar.HUM.Application.ChppFile.Download.Command.Factories
{
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Extract;
    using Hyperar.HUM.Shared.Enums;

    public class FileExtractStrategyFactory : IFileExtractStrategyFactory
    {
        private readonly CheckTokenExtractor checkTokenExtractor;

        private readonly EmptyExtractor emptyExtractor;

        private readonly ManagerCompendiumExtractor managerCompendiumExtractor;

        private readonly WorldDetailsExtractor worldDetailsExtractor;

        public FileExtractStrategyFactory(
            CheckTokenExtractor checkTokenExtractor,
            EmptyExtractor emptyExtractor,
            ManagerCompendiumExtractor managerCompendiumExtractor,
            WorldDetailsExtractor worldDetailsExtractor)
        {
            this.checkTokenExtractor = checkTokenExtractor;
            this.emptyExtractor = emptyExtractor;
            this.managerCompendiumExtractor = managerCompendiumExtractor;
            this.worldDetailsExtractor = worldDetailsExtractor;
        }

        public IFileExtractorStrategy GetFor(FileDownloadTaskBase fileDownloadTask)
        {
            if (fileDownloadTask is ImageFileDownloadTask)
            {
                return this.emptyExtractor;
            }
            else if (fileDownloadTask is XmlFileDownloadTask xmlFileDownloadTask)
            {
                return xmlFileDownloadTask.XmlFile switch
                {
                    XmlFileType.CheckToken => this.checkTokenExtractor,
                    XmlFileType.ManagerCompendium => this.managerCompendiumExtractor,
                    XmlFileType.WorldDetails => this.worldDetailsExtractor,
                    _ => this.emptyExtractor
                };
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(fileDownloadTask));
            }
        }
    }
}