namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;

    public class ImagePersister : IFilePersisterStrategy
    {
        public async Task PersistFileAsync(FileDownloadTaskBase fileDownloadTask, CancellationToken cancellationToken)
        {
            var imageFileDownloadTask = fileDownloadTask as ImageFileDownloadTask;

            ArgumentNullException.ThrowIfNull(imageFileDownloadTask);

            ArgumentNullException.ThrowIfNull(imageFileDownloadTask.ImageFileBytes);

            await ImageHelpers.WriteFileToCacheAsync(imageFileDownloadTask.Url, imageFileDownloadTask.ImageFileBytes, cancellationToken);
        }
    }
}