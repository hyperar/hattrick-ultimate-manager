namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Extract
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;

    public class EmptyExtractor : IFileExtractorStrategy
    {
        public Task ExecuteFileExtractionAsync(
            FileDownloadTaskBase fileDownloadTask,
            List<FileDownloadTaskBase> fileDownloadTasks,
            CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}