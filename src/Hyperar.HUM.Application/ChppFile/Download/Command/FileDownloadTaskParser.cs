namespace Hyperar.HUM.Application.ChppFile.Download.Command
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.Constants;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Parse.ExtensionMethods;
    using Hyperar.HUM.Shared.Enums;

    public class FileDownloadTaskParser : IFileDownloadTaskParser
    {
        private readonly IFileParseStrategyFactory fileParseStrategyFactory;

        public FileDownloadTaskParser(IFileParseStrategyFactory fileParseStrategyFactory)
        {
            this.fileParseStrategyFactory = fileParseStrategyFactory;
        }

        public async Task ExecuteAsync(
            FileDownloadTaskBase fileDownloadTask,
            List<FileDownloadTaskBase> fileDownloadTasks,
            CancellationToken cancellationToken)
        {
            try
            {
                var xmlFileDownloadTask = fileDownloadTask as XmlFileDownloadTask;

                ArgumentNullException.ThrowIfNull(xmlFileDownloadTask);

                ArgumentNullException.ThrowIfNull(xmlFileDownloadTask.FileContent);

                using (var memoryStream = new MemoryStream(xmlFileDownloadTask.FileContent))
                {
                    var xmlReaderSettings = new XmlReaderSettings
                    {
                        Async = true,
                        CloseInput = true,
                        IgnoreComments = true,
                        IgnoreProcessingInstructions = true,
                        IgnoreWhitespace = true
                    };

                    using (var xmlReader = XmlReader.Create(memoryStream, xmlReaderSettings))
                    {
                        xmlReader.ReadToFollowing(NodeName.FileName);

                        var fileName = await xmlReader.ReadElementContentAsStringAsync();
                        var version = (await xmlReader.ReadValueAsync()).AsDecimal();
                        var userId = (await xmlReader.ReadValueAsync()).AsLong();
                        var fetchedDate = (await xmlReader.ReadValueAsync()).AsDateTime();

                        var parser = this.fileParseStrategyFactory.GetFor(fileName);

                        await parser.ExecuteFileParseAsync(
                            xmlReader,
                            fileName,
                            version,
                            userId,
                            fetchedDate,
                            xmlFileDownloadTask,
                            cancellationToken);

                        fileDownloadTask.Status = DownloadTaskStatus.Read;
                    }
                }
            }
            catch (Exception ex)
            {
                fileDownloadTask.Status = DownloadTaskStatus.Error;
                fileDownloadTask.ErrorMessage = ex.Message;

                throw;
            }
        }
    }
}