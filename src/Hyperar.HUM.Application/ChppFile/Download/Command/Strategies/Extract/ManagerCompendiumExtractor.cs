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
    using Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium;

    public class ManagerCompendiumExtractor : XmlFileExtractorBase, IFileExtractorStrategy
    {
        public ManagerCompendiumExtractor(IFileDownloadTaskFactory fileDownloadTaskFactory) : base(fileDownloadTaskFactory)
        {
        }

        public async Task ExecuteFileExtractionAsync(
            FileDownloadTaskBase fileDownloadTask,
            List<FileDownloadTaskBase> fileDownloadTasks,
            CancellationToken cancellationToken)
        {
            var xmlFileDownloadTask = fileDownloadTask as XmlFileDownloadTask;

            ArgumentNullException.ThrowIfNull(xmlFileDownloadTask);

            var managerCompendium = xmlFileDownloadTask.Entity as HattrickData;

            ArgumentNullException.ThrowIfNull(managerCompendium);

            if (managerCompendium.Manager.Avatar is not null)
            {
                if (!ImageHelpers.ImageFileExists(
                    managerCompendium.Manager.Avatar.BackgroundImage))
                {
                    fileDownloadTasks.Insert(
                        fileDownloadTasks.IndexOf(xmlFileDownloadTask),
                        this.fileDownloadTaskFactory.BuildImageFileDownloadTask(
                            managerCompendium.Manager.Avatar.BackgroundImage));
                }

                fileDownloadTasks.InsertRange(
                    fileDownloadTasks.IndexOf(xmlFileDownloadTask),
                    await this.ExtractAvatarTasksAsync(
                        managerCompendium.Manager.Avatar));
            }

            fileDownloadTasks.Add(
                this.fileDownloadTaskFactory.BuildXmlFileDownloadTask(
                    xmlFileDownloadTask.UserProfileId,
                    XmlFileType.TeamDetails,
                    new NameValueCollection
                    {
                        { "UserId", managerCompendium.Manager.UserId.ToString() }
                    }));
        }
    }
}