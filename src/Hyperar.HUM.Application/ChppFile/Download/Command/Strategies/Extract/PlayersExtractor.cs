namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Extract
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Chpp.Players;

    public class PlayersExtractor : IFileExtractorStrategy
    {
        private readonly IFileDownloadTaskFactory fileDownloadTaskFactory;

        public PlayersExtractor(IFileDownloadTaskFactory fileDownloadTaskFactory)
        {
            this.fileDownloadTaskFactory = fileDownloadTaskFactory;
        }

        public Task ExecuteFileExtractionAsync(FileDownloadTaskBase fileDownloadTask, List<FileDownloadTaskBase> fileDownloadTasks, CancellationToken cancellationToken)
        {
            var xmlFileDownloadTask = fileDownloadTask as XmlFileDownloadTask;

            ArgumentNullException.ThrowIfNull(xmlFileDownloadTask);

            var players = xmlFileDownloadTask.Entity as HattrickData;

            ArgumentNullException.ThrowIfNull(players);

            fileDownloadTasks.Add(
                this.fileDownloadTaskFactory.BuildXmlFileDownloadTask(
                    xmlFileDownloadTask.UserProfileId,
                    XmlFileType.Avatars,
                    new NameValueCollection
                    {
                        { "TeamId", players.Team.TeamId.ToString() },
                    }));

            return Task.CompletedTask;
        }
    }
}