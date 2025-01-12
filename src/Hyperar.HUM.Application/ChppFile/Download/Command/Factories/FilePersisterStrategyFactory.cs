namespace Hyperar.HUM.Application.ChppFile.Download.Command.Factories
{
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist;
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

        public IFilePersisterStrategy GetFor(XmlFileType xmlFile)
        {
            return xmlFile switch
            {
                XmlFileType.ManagerCompendium => this.managerCompendiumPersister,
                XmlFileType.WorldDetails => this.worldDetailsPersister,
                _ => this.emptyPersister
            };
        }
    }
}