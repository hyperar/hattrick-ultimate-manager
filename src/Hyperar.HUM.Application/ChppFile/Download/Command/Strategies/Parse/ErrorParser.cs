namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;
    using Hyperar.HUM.Shared.Models.Chpp.Error;

    public class ErrorParser : IFileParseStrategy
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
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsInt(),
                (await xmlReader.ReadValueAsync()).AsGuid(),
                (await xmlReader.ReadValueAsync()).AsString(),
                (await xmlReader.ReadValueAsync()).AsInt());
        }
    }
}