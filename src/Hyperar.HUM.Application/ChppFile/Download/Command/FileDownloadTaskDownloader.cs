namespace Hyperar.HUM.Application.ChppFile.Download.Command
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Download;
    using Hyperar.HUM.Shared.Enums;

    public class FileDownloadTaskDownloader : IFileDownloadTaskDownloader
    {
        private readonly CheckTokenDownloader checkTokenDownloader;

        private readonly ImageFileDownloader imageFileDownloader;

        private readonly ProtectedResourceDownloader protectedResourceDownloader;

        public FileDownloadTaskDownloader(
            CheckTokenDownloader checkTokenDownloader,
            ImageFileDownloader imageFileDownloader,
            ProtectedResourceDownloader protectedResourceDownloader)
        {
            this.checkTokenDownloader = checkTokenDownloader;
            this.imageFileDownloader = imageFileDownloader;
            this.protectedResourceDownloader = protectedResourceDownloader;
        }

        public async Task ExecuteAsync(
            FileDownloadTaskBase fileDownloadTask,
            List<FileDownloadTaskBase> fileDownloadTasks,
            CancellationToken cancellationToken)
        {
            try
            {
                IFileDownloaderStrategy? downloader = null;

                if (fileDownloadTask is ImageFileDownloadTask)
                {
                    downloader = this.imageFileDownloader;
                }
                else if (fileDownloadTask is XmlFileDownloadTask xmlFileDownloadTask)
                {
                    downloader = xmlFileDownloadTask.XmlFile == XmlFileType.CheckToken
                        ? this.checkTokenDownloader
                        : this.protectedResourceDownloader;
                }

                ArgumentNullException.ThrowIfNull(downloader);

                await downloader.ExecuteFileDownloadAsync(fileDownloadTask, cancellationToken);
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