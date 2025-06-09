namespace Hyperar.HUM.Application.ChppFile.Download.Command.Factories
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.Constants;

    public class FileParseStrategyFactory : IFileParseStrategyFactory
    {
        private readonly AvatarsParser avatarsParser;

        private readonly CheckTokenParser checkTokenParser;

        private readonly ErrorParser errorParser;

        private readonly ManagerCompendiumParser managerCompendiumParser;

        private readonly PlayersParser playersParser;

        private readonly TeamDetailsParser teamDetailsParser;

        private readonly WorldDetailsParser worldDetailsParser;

        public FileParseStrategyFactory(
            AvatarsParser avatarsParser,
            CheckTokenParser checkTokenParser,
            ErrorParser errorParser,
            ManagerCompendiumParser managerCompendiumParser,
            PlayersParser playersParser,
            TeamDetailsParser teamDetailsParser,
            WorldDetailsParser worldDetailsParser)
        {
            this.avatarsParser = avatarsParser;
            this.checkTokenParser = checkTokenParser;
            this.errorParser = errorParser;
            this.managerCompendiumParser = managerCompendiumParser;
            this.playersParser = playersParser;
            this.teamDetailsParser = teamDetailsParser;
            this.worldDetailsParser = worldDetailsParser;
        }

        public IFileParseStrategy GetFor(string fileName)
        {
            return fileName.ToLower() switch
            {
                FileName.Avatars => this.avatarsParser,
                FileName.CheckToken => this.checkTokenParser,
                FileName.Error => this.errorParser,
                FileName.ManagerCompendium => this.managerCompendiumParser,
                FileName.Players => this.playersParser,
                FileName.TeamDetails => this.teamDetailsParser,
                FileName.WorldDetails => this.worldDetailsParser,
                _ => throw new ArgumentOutOfRangeException(nameof(fileName))
            };
        }
    }
}