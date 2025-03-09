namespace Hyperar.HUM.Application.ChppFile.Download.Command
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Download;
    using MediatR;

    internal class DownloadCommandHandler : IRequestHandler<DownloadCommand, bool>
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IFileDownloadTaskFactory fileDownloadTaskFactory;

        private readonly IFileDownloadTaskStepFactory fileDownloadTaskStepFactory;

        private readonly IRepository<Domain.UserProfile> userProfileRepository;

        public DownloadCommandHandler(
            IDatabaseContext databaseContext,
            IRepository<Domain.UserProfile> userProfileRepository,
            IFileDownloadTaskFactory fileDownloadTaskFactory,
            IFileDownloadTaskStepFactory fileDownloadTaskStepFactory)
        {
            this.databaseContext = databaseContext;
            this.userProfileRepository = userProfileRepository;
            this.fileDownloadTaskFactory = fileDownloadTaskFactory;
            this.fileDownloadTaskStepFactory = fileDownloadTaskStepFactory;
        }

        public async Task<bool> Handle(DownloadCommand request, CancellationToken cancellationToken)
        {
            var success = true;
            var isDownloading = true;

            var downloadTasks = new List<FileDownloadTaskBase>
            {
                this.fileDownloadTaskFactory.BuildXmlFileDownloadTask(
                    request.UserProfileId,
                    XmlFileType.CheckToken)
            };

            try
            {
                var userProfile = await this.userProfileRepository.GetByIdAsync(request.UserProfileId);

                ArgumentNullException.ThrowIfNull(userProfile);

                await this.databaseContext.BeginTransactionAsync();

                for (var i = 0; i < downloadTasks.Count; i++)
                {
                    while (downloadTasks[i].Status != DownloadTaskStatus.Finished)
                    {
                        /*
                         * IMPORTANT:
                         * CurrentTask are created here because IFileDownloadTaskExtractor can insert new tasks at the current index
                         * and those new tasks need to be finished before the original task gets to IFileDownloadTaskPersister step.
                         */
                        var currentTask = downloadTasks[i];

                        // Get Step.
                        var fileDownloadTaskStep = this.fileDownloadTaskStepFactory.GetNextStep(currentTask.Status);

                        // Execute Step.
                        await fileDownloadTaskStep.ExecuteAsync(
                            currentTask,
                            downloadTasks,
                            cancellationToken);

                        // Report Progress.
                        request.Progress.Report(
                            new DownloadReport(
                                downloadTasks.Select(ToDownloadTask)
                                    .ToList(),
                                ToDownloadTask(currentTask),
                                downloadTasks.Count(x => x.Status == DownloadTaskStatus.Finished),
                                downloadTasks.Count,
                                isDownloading));
                    }
                }

                userProfile.LastDownloadDate = DateTime.Now;

                await this.userProfileRepository.UpdateAsync(userProfile);
            }
            catch
            {
                downloadTasks.Where(x => x.Status is not DownloadTaskStatus.Finished
                                      and not DownloadTaskStatus.Error)
                    .ToList()
                    .ForEach(x => x.Status = DownloadTaskStatus.Canceled);

                success = false;

                this.databaseContext.Cancel();
            }
            finally
            {
                await this.databaseContext.EndTransactionAsync();

                isDownloading = false;

                // Report Progress.
                request.Progress.Report(
                    new DownloadReport(
                        downloadTasks.Select(ToDownloadTask)
                            .ToList(),
                        null,
                        downloadTasks.Count(x => x.Status == DownloadTaskStatus.Finished),
                        downloadTasks.Count,
                        isDownloading));
            }

            return success;
        }

        private static DownloadTask ToDownloadTask(FileDownloadTaskBase task)
        {
            return new DownloadTask(
                task.Id,
                task.FileType,
                task.Title,
                task.Status,
                task.ErrorMessage);
        }
    }
}