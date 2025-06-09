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
    using Hyperar.HUM.Application.Exceptions;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;
    using Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium;

    public class ManagerCompendiumParser : XmlParserBase, IFileParseStrategy
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
                await ReadManagerNodeAsync(
                    xmlReader));
        }

        private static async Task<Currency> ReadCurrencyNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var currency = new Currency(
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsDecimal());

            await xmlReader.ReadAsync();

            return currency;
        }

        private static async Task<string[]> ReadLastLoginsNodeAsync(XmlReader xmlReader)
        {
            var lastLogins = new List<string>();

            if (!xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();

                while (xmlReader.CheckNode(NodeName.LoginTime))
                {
                    lastLogins.Add(
                        (await xmlReader.ReadValueAsync()).AsString());
                }
            }

            await xmlReader.ReadAsync();

            return lastLogins.ToArray();
        }

        private static async Task<League> ReadLeagueNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var league = new League(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsInt());

            await xmlReader.ReadAsync();

            return league;
        }

        private static async Task<Manager> ReadManagerNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var manager = new Manager(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString(),
                await ReadLastLoginsNodeAsync(
                    xmlReader),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.Language),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.Country),
                await ReadCurrencyNodeAsync(
                    xmlReader),
                await ReadTeamsNodeAsync(
                    xmlReader),
                await ReadNullableIdNameListNodeAsync(
                    xmlReader,
                    NodeName.NationalTeamCoach,
                    NodeName.NationalTeam),
                await ReadNullableIdNameListNodeAsync(
                    xmlReader,
                    NodeName.NationalTeamAssistant,
                    NodeName.NationalTeam),
                await ReadNullableAvatarNodeAsync(
                    xmlReader));

            await xmlReader.ReadAsync();

            return manager;
        }

        private static async Task<Team> ReadTeamNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var team = new Team(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.Arena),
                await ReadLeagueNodeAsync(
                    xmlReader),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.Country),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.LeagueLevelUnit),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.Region),
                await ReadYouthTeamNodeAsync(
                    xmlReader));

            await xmlReader.ReadAsync();

            return team;
        }

        private static async Task<Team[]> ReadTeamsNodeAsync(
            XmlReader xmlReader)
        {
            if (!xmlReader.CheckNode(NodeName.Teams))
            {
                throw new ParserException(
                    string.Format(
                        Globalization.ErrorMessages.InvalidXmlElement,
                        NodeName.Teams,
                        xmlReader.Name));
            }

            await xmlReader.ReadAsync();

            var teamList = new List<Team>();

            while (xmlReader.CheckNode(NodeName.Team))
            {
                teamList.Add(
                    await ReadTeamNodeAsync(
                        xmlReader));
            }

            await xmlReader.ReadAsync();

            return teamList.ToArray();
        }

        private static async Task<YouthTeam?> ReadYouthTeamNodeAsync(XmlReader xmlReader)
        {
            YouthTeam? youthTeam = null;

            if (!xmlReader.IsEmptyElement)
            {
                await xmlReader.ReadAsync();

                youthTeam = new YouthTeam(
                   (await xmlReader.ReadValueAsync()).AsLong(),
                   (await xmlReader.ReadValueAsync()).AsString(),
                   await ReadNullableIdNameNodeAsync(
                       xmlReader,
                       NodeName.YouthLeague));
            }

            await xmlReader.ReadAsync();

            return youthTeam;
        }
    }
}