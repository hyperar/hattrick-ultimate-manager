namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Extract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Shared.Models.Chpp;
    using Hyperar.HUM.Shared.Models.Chpp.Avatars;

    public class AvatarsExtractor : IFileExtractorStrategy
    {
        private readonly IFileDownloadTaskFactory fileDownloadTaskFactory;

        public AvatarsExtractor(IFileDownloadTaskFactory fileDownloadTaskFactory)
        {
            this.fileDownloadTaskFactory = fileDownloadTaskFactory;
        }

        public Task ExecuteFileExtractionAsync(FileDownloadTaskBase fileDownloadTask, List<FileDownloadTaskBase> fileDownloadTasks, CancellationToken cancellationToken)
        {
            var xmlFileDownloadTask = fileDownloadTask as XmlFileDownloadTask;

            ArgumentNullException.ThrowIfNull(xmlFileDownloadTask);

            var avatars = xmlFileDownloadTask.Entity as HattrickData;

            ArgumentNullException.ThrowIfNull(avatars);

            fileDownloadTasks.InsertRange(
                fileDownloadTasks.IndexOf(xmlFileDownloadTask),
                avatars.Team.Players.Select(x => x.Avatar.BackgroundImage)
                    .Distinct()
                    .Where(x => !ImageHelper.ImageFileExists(x))
                    .Select(
                        this.fileDownloadTaskFactory.BuildImageFileDownloadTask));

            fileDownloadTasks.InsertRange(
                fileDownloadTasks.IndexOf(xmlFileDownloadTask),
                avatars.Team.Players.Where(x => x.Avatar.Layers is not null)
                    .SelectMany(x => x.Avatar.Layers ?? Array.Empty<Layer>())
                    .Select(x => x.Image)
                    .Distinct()
                    .Where(x => !ImageHelper.ImageFileExists(x))
                    .Select(
                        this.fileDownloadTaskFactory.BuildImageFileDownloadTask));

            return Task.CompletedTask;
        }
    }
}