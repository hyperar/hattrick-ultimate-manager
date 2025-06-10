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
    using Hyperar.HUM.Shared.Models.Chpp.Avatars;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;

    public class AvatarsParser : XmlParserBase, IFileParseStrategy
    {
        public async Task<IXmlFileBase> ExecuteFileParseAsync(
            XmlReader xmlReader,
            string fileName,
            decimal version,
            long userId,
            DateTime fetchedDate,
            CancellationToken cancellationToken)
        {
            return new HattrickData(
                fileName,
                version,
                userId,
                fetchedDate,
                await ReadTeamNodeAsync(xmlReader));
        }

        private static async Task<Player> ReadPlayerNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var player = new Player(
                (await xmlReader.ReadValueAsync()).AsLong(),
                await ReadAvatarNodeAsync(xmlReader));

            await xmlReader.ReadAsync();

            return player;
        }

        private static async Task<Player[]> ReadPlayersNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var result = new List<Player>();

            while (xmlReader.CheckNode(NodeName.Player))
            {
                result.Add(
                    await ReadPlayerNodeAsync(
                        xmlReader));
            }

            await xmlReader.ReadAsync();

            return result.ToArray();
        }

        private static async Task<Team> ReadTeamNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var team = new Team(
                (await xmlReader.ReadValueAsync()).AsLong(),
                 await ReadPlayersNodeAsync(xmlReader));

            await xmlReader.ReadAsync();

            return team;
        }
    }
}