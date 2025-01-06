﻿namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;
    using Hyperar.HUM.Shared.Models.Chpp.CheckToken;

    public class CheckTokenParser : IFileParseStrategy
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
                await xmlReader.ReadStringAsync(),
                await xmlReader.ReadDateTimeAsync(),
                await xmlReader.ReadLongAsync(),
                await xmlReader.ReadDateTimeAsync(),
                (await xmlReader.ReadStringAsync()).Split(",", StringSplitOptions.RemoveEmptyEntries));
        }
    }
}