namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Extract
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Shared.Enums;

    public class CheckTokenExtractor : IFileExtractorStrategy
    {
        private readonly IFileDownloadTaskFactory fileDownloadTaskFactory;

        public CheckTokenExtractor(IFileDownloadTaskFactory fileDownloadTaskFactory)
        {
            this.fileDownloadTaskFactory = fileDownloadTaskFactory;
        }

        public Task ExecuteFileExtractionAsync(
            FileDownloadTaskBase fileDownloadTask,
            List<FileDownloadTaskBase> fileDownloadTasks,
            CancellationToken cancellationToken)
        {
            var xmlFileDownloadTask = fileDownloadTask as XmlFileDownloadTask;

            ArgumentNullException.ThrowIfNull(xmlFileDownloadTask);

            fileDownloadTasks.AddRange(
                new[]
                {
                    this.fileDownloadTaskFactory.BuildXmlFileDownloadTask(
                        xmlFileDownloadTask.UserProfileId,
                        XmlFileType.WorldDetails),
                    this.fileDownloadTaskFactory.BuildXmlFileDownloadTask(
                        xmlFileDownloadTask.UserProfileId,
                        XmlFileType.ManagerCompendium)
                });

            return Task.CompletedTask;
        }
    }
}