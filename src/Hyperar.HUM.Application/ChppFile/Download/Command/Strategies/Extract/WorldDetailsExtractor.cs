namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Extract
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Shared.Models.Chpp.WorldDetails;

    public class WorldDetailsExtractor : IFileExtractorStrategy
    {
        private readonly IFileDownloadTaskFactory fileDownloadTaskFactory;

        public WorldDetailsExtractor(IFileDownloadTaskFactory fileDownloadTaskFactory)
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

            var worldDetails = xmlFileDownloadTask.Entity as HattrickData;

            ArgumentNullException.ThrowIfNull(worldDetails);

            var imageList = worldDetails.LeagueList.Select(x => string.Format(ImageHelpers.FlagUrlMask, x.LeagueId));

            fileDownloadTasks.InsertRange(
                fileDownloadTasks.IndexOf(xmlFileDownloadTask),
                imageList.Where(x => !ImageHelpers.ImageFileExists(x))
                    .Select(
                        this.fileDownloadTaskFactory.BuildImageFileDownloadTask));

            return Task.CompletedTask;
        }
    }
}