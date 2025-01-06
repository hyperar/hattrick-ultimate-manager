namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Download
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Shared.Enums;

    public class ImageFileDownloader : IFileDownloaderStrategy
    {
        public async Task ExecuteFileDownloadAsync(FileDownloadTaskBase fileDownloadTask, CancellationToken cancellationToken)
        {
            var task = fileDownloadTask as ImageFileDownloadTask;

            ArgumentNullException.ThrowIfNull(task);

            if (!ImageHelpers.ImageFileExists(task.Url))
            {
                var finalUrl = ImageHelpers.NormalizeUrl(task.Url);

                byte[]? fileContent = null;

                using (var httpClient = new HttpClient())
                {
                    fileContent = await httpClient.GetByteArrayAsync(finalUrl, cancellationToken);
                }

                ArgumentNullException.ThrowIfNull(fileContent);

                await ImageHelpers.WriteFileToCacheAsync(task.Url, fileContent, cancellationToken);
            }

            fileDownloadTask.Status = DownloadTaskStatus.Finished;
        }
    }
}