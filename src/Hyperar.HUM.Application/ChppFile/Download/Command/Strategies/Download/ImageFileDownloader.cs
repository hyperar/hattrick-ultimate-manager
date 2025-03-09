namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Download
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Shared.Enums;

    public class ImageFileDownloader : IFileDownloaderStrategy
    {
        public async Task ExecuteFileDownloadAsync(FileDownloadTaskBase fileDownloadTask, CancellationToken cancellationToken)
        {
            var imageFileDownloadTask = fileDownloadTask as ImageFileDownloadTask;

            ArgumentNullException.ThrowIfNull(imageFileDownloadTask);

            if (!ImageHelpers.ImageFileExists(imageFileDownloadTask.Url))
            {
                using (var httpClient = new HttpClient())
                {
                    imageFileDownloadTask.ImageFileBytes = await httpClient.GetByteArrayAsync(
                        imageFileDownloadTask.Url,
                        cancellationToken);
                }

                ArgumentNullException.ThrowIfNull(imageFileDownloadTask.ImageFileBytes);
            }

            fileDownloadTask.Status = DownloadTaskStatus.Processed;
        }
    }
}