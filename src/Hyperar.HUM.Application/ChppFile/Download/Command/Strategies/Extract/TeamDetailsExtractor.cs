namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Extract
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Chpp.TeamDetails;

    public class TeamDetailsExtractor : IFileExtractorStrategy
    {
        private readonly IFileDownloadTaskFactory fileDownloadTaskFactory;

        public TeamDetailsExtractor(IFileDownloadTaskFactory fileDownloadTaskFactory)
        {
            this.fileDownloadTaskFactory = fileDownloadTaskFactory;
        }

        public Task ExecuteFileExtractionAsync(FileDownloadTaskBase fileDownloadTask, List<FileDownloadTaskBase> fileDownloadTasks, CancellationToken cancellationToken)
        {
            var xmlFileDownloadTask = fileDownloadTask as XmlFileDownloadTask;

            ArgumentNullException.ThrowIfNull(xmlFileDownloadTask);

            var teamDetails = xmlFileDownloadTask.Entity as HattrickData;

            ArgumentNullException.ThrowIfNull(teamDetails);

            var leagueIds = teamDetails.Teams.Select(x => x.League.Id)
                .Distinct();

            foreach (var curLeagueId in leagueIds)
            {
                fileDownloadTasks.Insert(
                    fileDownloadTasks.IndexOf(xmlFileDownloadTask),
                    this.fileDownloadTaskFactory.BuildXmlFileDownloadTask(
                        xmlFileDownloadTask.UserProfileId,
                        XmlFileType.WorldDetails,
                        new NameValueCollection
                        {
                            { "LeagueId", curLeagueId.ToString() },
                            { "IncludeRegions", bool.TrueString }
                        }));
            }

            foreach (var curTeam in teamDetails.Teams)
            {
                fileDownloadTasks.Add(
                    this.fileDownloadTaskFactory.BuildXmlFileDownloadTask(
                        xmlFileDownloadTask.UserProfileId,
                        XmlFileType.Players,
                        new NameValueCollection
                        {
                            { "TeamId", curTeam.TeamId.ToString() }
                        }));
            }

            fileDownloadTasks.InsertRange(
                fileDownloadTasks.IndexOf(xmlFileDownloadTask),
                teamDetails.Teams.Select(x => x.DressUri)
                    .Where(x => !ImageHelpers.ImageFileExists(x))
                    .Select(
                        this.fileDownloadTaskFactory.BuildImageFileDownloadTask));

            fileDownloadTasks.InsertRange(
                fileDownloadTasks.IndexOf(xmlFileDownloadTask),
                teamDetails.Teams.Select(x => x.AlternateDressUri)
                    .Where(x => !ImageHelpers.ImageFileExists(x))
                    .Select(
                        this.fileDownloadTaskFactory.BuildImageFileDownloadTask));

            fileDownloadTasks.InsertRange(
                fileDownloadTasks.IndexOf(xmlFileDownloadTask),
                teamDetails.Teams.Select(x => x.LogoUrl)
                    .Where(x => !string.IsNullOrWhiteSpace(x)
                             && !ImageHelpers.ImageFileExists(x))
                    .Select(
                        this.fileDownloadTaskFactory.BuildImageFileDownloadTask));

            return Task.CompletedTask;
        }
    }
}