namespace Hyperar.HUM.Application.ChppFile.Download.Command.Factories
{
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Application.Exceptions;
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

        public IFileDownloadTaskStep GetNextStep(FileDownloadTaskBase fileDownloadTask)
        {
            return fileDownloadTask.Status switch
            {
                DownloadTaskStatus.Pending => this.fileDownloadTaskDownloader,
                DownloadTaskStatus.Downloaded => this.fileDownloadTaskParser,
                DownloadTaskStatus.Read => this.fileDownloadTaskExtractor,
                DownloadTaskStatus.Processed => this.fileDownloadTaskPersister,
                _ => throw new BusinessException(
                    string.Format(
                        Globalization.ErrorMessages.ValueOutOfRange,
                        fileDownloadTask.Status,
                        nameof(fileDownloadTask.Status)))
            };
        }
    }
}