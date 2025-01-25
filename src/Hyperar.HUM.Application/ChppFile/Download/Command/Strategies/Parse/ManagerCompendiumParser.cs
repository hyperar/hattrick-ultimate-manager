namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.Constants;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;
    using Hyperar.HUM.Application.Exceptions;
    using Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium;

    public class ManagerCompendiumParser : XmlParserBase, IFileParseStrategy
    {
        public async Task ExecuteFileParseAsync(
            XmlReader xmlReader,
            string fileName,
            decimal version,
            long userId,
            DateTime fetchetDate,
            XmlFileDownloadTask xmlFileDownloadTask,
            CancellationToken cancellationToken)
        {
            xmlFileDownloadTask.Entity = new HattrickData(
                fileName,
                version,
                userId,
                fetchetDate,
                await ReadManagerNodeAsync(
                    xmlReader,
                    cancellationToken));
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

        private static async Task<IEnumerable<string>> ReadLastLoginsNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var lastLogins = new List<string>();

            while (xmlReader.CheckNode(NodeName.LoginTime))
            {
                lastLogins.Add(
                    (await xmlReader.ReadValueAsync()).AsString());
            }

            await xmlReader.ReadAsync();

            return lastLogins;
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

        private static async Task<Manager> ReadManagerNodeAsync(XmlReader xmlReader, CancellationToken cancellationToken)
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
                    NodeName.Language,
                    cancellationToken),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.Country,
                    cancellationToken),
                await ReadCurrencyNodeAsync(
                    xmlReader),
                await ReadTeamsNodeAsync(
                    xmlReader,
                    cancellationToken),
                await ReadNullableIdNameListNodeAsync(
                    xmlReader,
                    NodeName.NationalTeamCoach,
                    NodeName.NationalTeam,
                    cancellationToken),
                await ReadNullableIdNameListNodeAsync(
                    xmlReader,
                    NodeName.NationalTeamAssistant,
                    NodeName.NationalTeam,
                    cancellationToken),
                await ReadAvatarNodeAsync(
                    xmlReader,
                    cancellationToken));

            await xmlReader.ReadAsync();

            return manager;
        }

        private static async Task<Team> ReadTeamNodeAsync(XmlReader xmlReader, CancellationToken cancellationToken)
        {
            await xmlReader.ReadAsync();

            var team = new Team(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.Arena,
                    cancellationToken),
                await ReadLeagueNodeAsync(
                    xmlReader),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.Country,
                    cancellationToken),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.LeagueLevelUnit,
                    cancellationToken),
                await ReadIdNameNodeAsync(
                    xmlReader,
                    NodeName.Region,
                    cancellationToken),
                await ReadYouthTeamNodeAsync(
                    xmlReader,
                    cancellationToken));

            await xmlReader.ReadAsync();

            return team;
        }

        private static async Task<IEnumerable<Team>> ReadTeamsNodeAsync(
            XmlReader xmlReader,
            CancellationToken cancellationToken)
        {
            if (!xmlReader.CheckNode(NodeName.Teams))
            {
                throw new BusinessException(
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
                        xmlReader,
                        cancellationToken));
            }

            await xmlReader.ReadAsync();

            return teamList;
        }

        private static async Task<YouthTeam?> ReadYouthTeamNodeAsync(XmlReader xmlReader, CancellationToken cancellationToken)
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
                       NodeName.YouthLeague,
                       cancellationToken));
            }

            await xmlReader.ReadAsync();

            return youthTeam;
        }
    }
}