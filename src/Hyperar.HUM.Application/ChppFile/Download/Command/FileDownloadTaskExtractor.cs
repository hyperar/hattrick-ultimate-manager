namespace Hyperar.HUM.Application.ChppFile.Download.Command
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Shared.Enums;

    public class FileDownloadTaskExtractor : IFileDownloadTaskExtractor
    {
        private readonly IFileExtractStrategyFactory fileExtractStrategyFactory;

        public FileDownloadTaskExtractor(IFileExtractStrategyFactory fileExtractStrategyFactory)
        {
            this.fileExtractStrategyFactory = fileExtractStrategyFactory;
        }

        public async Task ExecuteAsync(
            FileDownloadTaskBase fileDownloadTask,
            List<FileDownloadTaskBase> fileDownloadTasks,
            CancellationToken cancellationToken)
        {
            try
            {
                var extractor = this.fileExtractStrategyFactory.GetFor(fileDownloadTask);

                await extractor.ExecuteFileExtractionAsync(fileDownloadTask, fileDownloadTasks, cancellationToken);

                fileDownloadTask.Status = DownloadTaskStatus.Processed;
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