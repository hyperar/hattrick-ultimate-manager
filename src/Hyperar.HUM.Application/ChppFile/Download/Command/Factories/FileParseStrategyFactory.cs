namespace Hyperar.HUM.Application.ChppFile.Download.Command.Factories
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.Constants;

    public class FileParseStrategyFactory : IFileParseStrategyFactory
    {
        private readonly CheckTokenParser checkTokenParser;

        private readonly ErrorParser errorParser;

        private readonly ManagerCompendiumParser managerCompendiumParser;

        private readonly WorldDetailsParser worldDetailsParser;

        public FileParseStrategyFactory(
            CheckTokenParser checkTokenParser,
            ErrorParser errorParser,
            ManagerCompendiumParser managerCompendiumParser,
            WorldDetailsParser worldDetailsParser)
        {
            this.checkTokenParser = checkTokenParser;
            this.errorParser = errorParser;
            this.managerCompendiumParser = managerCompendiumParser;
            this.worldDetailsParser = worldDetailsParser;
        }

        public IFileParseStrategy GetFor(string fileName)
        {
            return fileName.ToLower() switch
            {
                FileName.CheckToken => this.checkTokenParser,
                FileName.Error => this.errorParser,
                FileName.ManagerCompendium => this.managerCompendiumParser,
                FileName.WorldDetails => this.worldDetailsParser,
                _ => throw new ArgumentOutOfRangeException(nameof(fileName))
            };
        }
    }
}