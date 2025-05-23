﻿namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse
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
    using Hyperar.HUM.Shared.Models.Chpp.WorldDetails;

    public class WorldDetailsParser : XmlParserBase, IFileParseStrategy
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
                await ReadLeagueListNodeAsync(
                    xmlReader));
        }

        private static async Task<Country> ReadCountryNodeAsync(XmlReader xmlReader)
        {
            if (!xmlReader.CheckNode(NodeName.Country))
            {
                throw new ParserException(
                    string.Format(
                        Globalization.ErrorMessages.InvalidXmlElement,
                        NodeName.Country,
                        xmlReader.Name));
            }

            var available = xmlReader.GetAttribute(NodeName.Available).AsBool();

            await xmlReader.ReadAsync();

            var country = new Country(
                available,
                available ? (await xmlReader.ReadValueAsync()).AsLong() : null,
                available ? (await xmlReader.ReadValueAsync()).AsString() : null,
                available ? (await xmlReader.ReadValueAsync()).AsString() : null,
                available ? (await xmlReader.ReadValueAsync()).AsDecimal() : null,
                available ? (await xmlReader.ReadValueAsync()).AsString() : null,
                available ? (await xmlReader.ReadValueAsync()).AsString() : null,
                available ? (await xmlReader.ReadValueAsync()).AsString() : null,
                available && xmlReader.CheckNode(NodeName.RegionList) ?
                    await ReadIdNameListNodeAsync(xmlReader, NodeName.RegionList, NodeName.Region) :
                    null);

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
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt());

            await xmlReader.ReadAsync();

            return cup;
        }

        private static async Task<Cup[]> ReadCupsNodeAsync(XmlReader xmlReader)
        {
            if (!xmlReader.CheckNode(NodeName.Cups))
            {
                throw new ParserException(
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

            return cups.ToArray();
        }

        private static async Task<League[]> ReadLeagueListNodeAsync(XmlReader xmlReader)
        {
            if (!xmlReader.CheckNode(NodeName.LeagueList))
            {
                throw new ParserException(
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
                        xmlReader));
            }

            await xmlReader.ReadAsync();

            return leagues.ToArray();
        }

        private static async Task<League> ReadLeagueNodeAsync(XmlReader xmlReader)
        {
            await xmlReader.ReadAsync();

            var league = new League(
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsString(),
                await ReadCountryNodeAsync(
                    xmlReader),
                await ReadCupsNodeAsync(
                    xmlReader),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsLong(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsDateTime(),
                (await xmlReader.ReadValueAsync()).AsInt());

            await xmlReader.ReadAsync();

            return league;
        }
    }
}