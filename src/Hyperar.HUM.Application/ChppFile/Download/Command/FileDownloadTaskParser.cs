namespace Hyperar.HUM.Application.ChppFile.Download.Command
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Shared.Enums;

    public class FileDownloadTaskParser : IFileDownloadTaskParser
    {
        private readonly IXmlFileParser xmlParser;

        public FileDownloadTaskParser(IXmlFileParser xmlParser)
        {
            this.xmlParser = xmlParser;
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

                xmlFileDownloadTask.Entity = await this.xmlParser.ParseXmlFileAsync(
                    xmlFileDownloadTask.FileContent,
                    cancellationToken);

                xmlFileDownloadTask.Status = DownloadTaskStatus.Read;
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