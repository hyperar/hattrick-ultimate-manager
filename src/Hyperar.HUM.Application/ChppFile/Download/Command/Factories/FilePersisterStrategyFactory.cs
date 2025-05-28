namespace Hyperar.HUM.Application.ChppFile.Download.Command.Factories
{
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist;
    using Hyperar.HUM.Application.Exceptions;
    using Hyperar.HUM.Shared.Enums;

    public class FilePersisterStrategyFactory : IFilePersisterStrategyFactory
    {
        private readonly EmptyPersister emptyPersister;

        private readonly ImagePersister imagePersister;

        private readonly ManagerCompendiumPersister managerCompendiumPersister;

        private readonly TeamDetailsPersister teamDetailsPersister;

        private readonly WorldDetailsPersister worldDetailsPersister;

        public FilePersisterStrategyFactory(
            EmptyPersister emptyPersister,
            ImagePersister imagePersister,
            ManagerCompendiumPersister managerCompendiumPersister,
            TeamDetailsPersister teamDetailsPersister,
            WorldDetailsPersister worldDetailsPersister)
        {
            this.emptyPersister = emptyPersister;
            this.managerCompendiumPersister = managerCompendiumPersister;
            this.teamDetailsPersister = teamDetailsPersister;
            this.worldDetailsPersister = worldDetailsPersister;
            this.imagePersister = imagePersister;
        }

        public IFilePersisterStrategy GetFor(FileDownloadTaskBase fileDownloadTask)
        {
            if (fileDownloadTask is ImageFileDownloadTask)
            {
                return this.imagePersister;
            }
            else if (fileDownloadTask is XmlFileDownloadTask xmlFileDownloadTask)
            {
                return xmlFileDownloadTask.XmlFile switch
                {
                    XmlFileType.ManagerCompendium => this.managerCompendiumPersister,
                    XmlFileType.TeamDetails => this.teamDetailsPersister,
                    XmlFileType.WorldDetails => this.worldDetailsPersister,
                    _ => this.emptyPersister
                };
            }
            else
            {
                throw new PersisterException(
                    string.Format(
                        Globalization.ErrorMessages.TypeOutOfRange,
                        fileDownloadTask.GetType().Name,
                        nameof(fileDownloadTask)));
            }
        }
    }
}