namespace Hyperar.HUM.Application.ChppFile.Download.Command.Factories
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Shared.Enums;

    public class FileDownloadTaskStepFactory : IFileDownloadTaskStepFactory
    {
        private readonly IFileDownloadTaskDownloader fileDownloadTaskDownloader;

        private readonly IFileDownloadTaskExtractor fileDownloadTaskExtractor;

        private readonly IFileDownloadTaskParser fileDownloadTaskParser;

        private readonly IFileDownloadTaskPersister fileDownloadTaskPersister;

        public FileDownloadTaskStepFactory(
            IFileDownloadTaskDownloader fileDownloadTaskDownloader,
            IFileDownloadTaskExtractor fileDownloadTaskExtractor,
            IFileDownloadTaskParser fileDownloadTaskParser,
            IFileDownloadTaskPersister fileDownloadTaskPersister)
        {
            this.fileDownloadTaskDownloader = fileDownloadTaskDownloader;
            this.fileDownloadTaskExtractor = fileDownloadTaskExtractor;
            this.fileDownloadTaskParser = fileDownloadTaskParser;
            this.fileDownloadTaskPersister = fileDownloadTaskPersister;
        }

        public IFileDownloadTaskStep GetNextStep(DownloadTaskStatus status)
        {
            return status switch
            {
                DownloadTaskStatus.Pending => this.fileDownloadTaskDownloader,
                DownloadTaskStatus.Downloaded => this.fileDownloadTaskParser,
                DownloadTaskStatus.Read => this.fileDownloadTaskExtractor,
                DownloadTaskStatus.Processed => this.fileDownloadTaskPersister,
                _ => throw new ArgumentOutOfRangeException(nameof(status))
            };
        }
    }
}