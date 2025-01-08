namespace Hyperar.HUM.Application.ChppFile.Download.Command.Factories
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist;
    using Hyperar.HUM.Application.Exceptions;
    using Hyperar.HUM.Shared.Enums;

    public class FilePersisterStrategyFactory : IFilePersisterStrategyFactory
    {
        private readonly EmptyPersister emptyPersister;

        private readonly ManagerCompendiumPersister managerCompendiumPersister;

        private readonly WorldDetailsPersister worldDetailsPersister;

        public FilePersisterStrategyFactory(
            EmptyPersister emptyPersister,
            ManagerCompendiumPersister managerCompendiumPersister,
            WorldDetailsPersister worldDetailsPersister)
        {
            this.emptyPersister = emptyPersister;
            this.managerCompendiumPersister = managerCompendiumPersister;
            this.worldDetailsPersister = worldDetailsPersister;
        }

        public IFilePersisterStrategy GetFor(FileDownloadTaskBase fileDownloadTask)
        {
            if (fileDownloadTask is XmlFileDownloadTask xmlFileDownloadTask)
            {
                return xmlFileDownloadTask.XmlFile switch
                {
                    XmlFileType.ManagerCompendium => this.managerCompendiumPersister,
                    XmlFileType.WorldDetails => this.worldDetailsPersister,
                    _ => this.emptyPersister
                };
            }
            else if (fileDownloadTask is ImageFileDownloadTask)
            {
                return this.emptyPersister;
            }
            else
            {
                throw new BusinessException(
                    string.Format(
                        Globalization.ErrorMessages.TypeOutOfRange,
                        fileDownloadTask.GetType(),
                        nameof(fileDownloadTask)));
            }
        }
    }
}