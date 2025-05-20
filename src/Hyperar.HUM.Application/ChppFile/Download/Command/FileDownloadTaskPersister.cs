namespace Hyperar.HUM.Application.ChppFile.Download.Command
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Enums;

    public class FileDownloadTaskPersister : IFileDownloadTaskPersister
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IFilePersisterStrategyFactory filePersisterStrategyFactory;

        public FileDownloadTaskPersister(
            IDatabaseContext databaseContext,
            IFilePersisterStrategyFactory filePersisterStrategyFactory)
        {
            this.filePersisterStrategyFactory = filePersisterStrategyFactory;
            this.databaseContext = databaseContext;
        }

        public async Task ExecuteAsync(
            FileDownloadTaskBase fileDownloadTask,
            List<FileDownloadTaskBase> fileDownloadTasks,
            CancellationToken cancellationToken)
        {
            try
            {
                var persister = this.filePersisterStrategyFactory.GetFor(fileDownloadTask);

                await persister.PersistFileAsync(fileDownloadTask, cancellationToken);

                if (fileDownloadTask is XmlFileDownloadTask)
                {
                    await this.databaseContext.SaveAsync();
                }

                fileDownloadTask.Status = DownloadTaskStatus.Finished;
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