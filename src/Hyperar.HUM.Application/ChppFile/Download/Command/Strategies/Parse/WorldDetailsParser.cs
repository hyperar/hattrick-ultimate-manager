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
    using Hyperar.HUM.Shared.Models.Chpp.WorldDetails;

    public class WorldDetailsParser : XmlParserBase, IFileParseStrategy
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
                await ReadLeagueListNodeAsync(
                    xmlReader,
                    cancellationToken));
        }

        private static async Task<Country> ReadCountryNodeAsync(XmlReader xmlReader, CancellationToken cancellationToken)
        {
            if (!xmlReader.CheckNode(NodeName.Country))
            {
                throw new BusinessException(
                    string.Format(
                        Globalization.ErrorMessages.InvalidXmlElement,
                        NodeName.Country,
                        xmlReader.Name));
            }

            var available = xmlReader.GetAttribute(NodeName.Available) == bool.TrueString;

            await xmlReader.ReadAsync();

            var country = new Country(
                available,
                available ? await xmlReader.ReadLongAsync() : null,
                available ? await xmlReader.ReadStringAsync() : null,
                available ? await xmlReader.ReadStringAsync() : null,
                available ? await xmlReader.ReadDecimalAsync() : null,
                available ? await xmlReader.ReadStringAsync() : null,
                available ? await xmlReader.ReadStringAsync() : null,
                available ? await xmlReader.ReadStringAsync() : null,
                await ReadNullableIdNameListNodeAsync(
                    xmlReader,
                    NodeName.RegionList,
                    NodeName.Region,
                    cancellationToken));

            if (available)
            {
                await xmlReader.ReadAsync();
            }

            return country;
        }

        private static async Task<Cup> ReadCupNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var cup = new Cup(
                await xmlReader.ReadLongAsync(),
                await xmlReader.ReadStringAsync(),
                await xmlReader.ReadIntAsync(),
                await xmlReader.ReadIntAsync(),
                await xmlReader.ReadIntAsync(),
                await xmlReader.ReadIntAsync(),
                await xmlReader.ReadIntAsync());

            await xmlReader.ReadAsync();

            return cup;
        }

        private static async Task<IEnumerable<Cup>> ReadCupsNodeAsync(XmlReader xmlReader)
        {
            if (!xmlReader.CheckNode(NodeName.Cups))
            {
                throw new BusinessException(
                    string.Format(
                        Globalization.ErrorMessages.InvalidXmlElement,
                        NodeName.Cups,
                        xmlReader.Name));
            }

            var cups = new List<Cup>();

            await xmlReader.ReadAsync();

            while (xmlReader.CheckNode(NodeName.Cup))
            {
                cups.Add(
                    await ReadCupNodeAsync(
                        xmlReader));
            }

            await xmlReader.ReadAsync();

            return cups;
        }

        private static async Task<IEnumerable<League>> ReadLeagueListNodeAsync(XmlReader xmlReader, CancellationToken cancellationToken)
        {
            if (!xmlReader.CheckNode(NodeName.LeagueList))
            {
                throw new BusinessException(
                    string.Format(
                        Globalization.ErrorMessages.InvalidXmlElement,
                        NodeName.LeagueList,
                        xmlReader.Name));
            }

            var leagues = new List<League>();

            await xmlReader.ReadAsync();

            while (xmlReader.CheckNode(NodeName.League))
            {
                leagues.Add(
                    await ReadLeagueNodeAsync(
                        xmlReader,
                        cancellationToken));
            }

            await xmlReader.ReadAsync();

            return leagues;
        }

        private static async Task<League> ReadLeagueNodeAsync(XmlReader xmlReader, CancellationToken cancellationToken)
        {
            await xmlReader.ReadAsync();

            var league = new League(
                await xmlReader.ReadLongAsync(),
                await xmlReader.ReadStringAsync(),
                await xmlReader.ReadIntAsync(),
                await xmlReader.ReadIntAsync(),
                await xmlReader.ReadIntAsync(),
                await xmlReader.ReadStringAsync(),
                await xmlReader.ReadStringAsync(),
                await xmlReader.ReadStringAsync(),
                await xmlReader.ReadStringAsync(),
                await xmlReader.ReadLongAsync(),
                await xmlReader.ReadStringAsync(),
                await ReadCountryNodeAsync(
                    xmlReader,
                    cancellationToken),
                await ReadCupsNodeAsync(
                    xmlReader),
                await xmlReader.ReadLongAsync(),
                await xmlReader.ReadLongAsync(),
                await xmlReader.ReadIntAsync(),
                await xmlReader.ReadIntAsync(),
                await xmlReader.ReadIntAsync(),
                await xmlReader.ReadDateTimeAsync(),
                await xmlReader.ReadDateTimeAsync(),
                await xmlReader.ReadDateTimeAsync(),
                await xmlReader.ReadDateTimeAsync(),
                await xmlReader.ReadDateTimeAsync(),
                await xmlReader.ReadDateTimeAsync(),
                await xmlReader.ReadDateTimeAsync(),
                await xmlReader.ReadDateTimeAsync(),
                await xmlReader.ReadDateTimeAsync(),
                await xmlReader.ReadIntAsync());

            await xmlReader.ReadAsync();

            return league;
        }
    }
}