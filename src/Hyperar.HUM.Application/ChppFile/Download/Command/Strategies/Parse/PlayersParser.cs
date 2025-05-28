namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.Constants;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;
    using Hyperar.HUM.Shared.Models.Chpp.Players;

    public class PlayersParser : IFileParseStrategy
    {
        public async Task<IXmlFileBase> ExecuteFileParseAsync(
            XmlReader xmlReader,
            string fileName,
            decimal version,
            long userId,
            DateTime fetchedDate,
            CancellationToken cancellationToken)
        {
            string userSupporter = (await xmlReader.ReadValueAsync()).AsString();
            bool isYouth = (await xmlReader.ReadValueAsync()).AsBool();
            string actionType = (await xmlReader.ReadValueAsync()).AsString();
            bool isPlayingMatch = (await xmlReader.ReadValueAsync()).AsBool();

            if (isPlayingMatch)
            {
                throw new InvalidOperationException();
            }

            return new HattrickData(
                fileName,
                version,
                userId,
                fetchedDate,
                userSupporter,
                isYouth,
                actionType,
                isPlayingMatch,
                await ReadTeamNodeAsync(xmlReader));
        }

        private static async Task<OwningTeam> ReadOwningTeamNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var owningteam = new OwningTeam(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString());

            await xmlReader.ReadAsync();

            return owningteam;
        }

        private static async Task<LastMatch> ReadLastMatchNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var lastmatch = new LastMatch(
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsDecimal(),
                (await xmlReader.ReadValueAsync()).AsDecimal());

            await xmlReader.ReadAsync();

            return lastmatch;
        }

        private static async Task<TrainerData> ReadTrainerDataNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var trainerdata = new TrainerData(
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt());

            await xmlReader.ReadAsync();

            return trainerdata;
        }

        private static async Task<Team> ReadTeamNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var team = new Team(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                xmlReader.CheckNode(NodeName.PlayerList) ? (await ReadPlayerListNodeAsync(xmlReader)) : null);

            await xmlReader.ReadAsync();

            return team;
        }

        private static async Task<Player[]> ReadPlayerListNodeAsync(XmlReader xmlReader)
        {
            var players = new List<Player>();

            if (xmlReader.IsEmptyElement)
            {
                return players.ToArray();
            }

            await xmlReader.ReadAsync();

            while (xmlReader.CheckNode(NodeName.Player))
            {
                players.Add(
                    await ReadPlayerNodeAsync(
                        xmlReader));
            }

            await xmlReader.ReadAsync();

            return players.ToArray();
        }

        private static async Task<Player> ReadPlayerNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var player = new Player(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsBool(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsBool(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsBool(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                xmlReader.CheckNode(NodeName.StaminaSkill) ? (await xmlReader.ReadValueAsync()).AsNullableInt() : null,
                xmlReader.CheckNode(NodeName.KeeperSkill) ? (await xmlReader.ReadValueAsync()).AsNullableInt() : null,
                xmlReader.CheckNode(NodeName.PlaymakerSkill) ? (await xmlReader.ReadValueAsync()).AsNullableInt() : null,
                xmlReader.CheckNode(NodeName.ScorerSkill) ? (await xmlReader.ReadValueAsync()).AsNullableInt() : null,
                xmlReader.CheckNode(NodeName.PassingSkill) ? (await xmlReader.ReadValueAsync()).AsNullableInt() : null,
                xmlReader.CheckNode(NodeName.WingerSkill) ? (await xmlReader.ReadValueAsync()).AsNullableInt() : null,
                xmlReader.CheckNode(NodeName.DefenderSkill) ? (await xmlReader.ReadValueAsync()).AsNullableInt() : null,
                xmlReader.CheckNode(NodeName.SetPiecesSkill) ? (await xmlReader.ReadValueAsync()).AsNullableInt() : null,
                xmlReader.CheckNode(NodeName.PlayerCategoryId) ? (await xmlReader.ReadValueAsync()).AsNullableInt() : null,
                xmlReader.CheckNode(NodeName.OwningTeam) ? (await ReadOwningTeamNodeAsync(xmlReader)) : null,
                xmlReader.CheckNode(NodeName.TrainerData) ? (await ReadTrainerDataNodeAsync(xmlReader)) : null,
                xmlReader.CheckNode(NodeName.LastMatch) ? (await ReadLastMatchNodeAsync(xmlReader)) : null);

            await xmlReader.ReadAsync();

            return player;
        }
    }
}