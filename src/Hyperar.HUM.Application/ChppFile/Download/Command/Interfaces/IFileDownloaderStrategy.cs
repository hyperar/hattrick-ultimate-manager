namespace Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;

    public interface IFileDownloaderStrategy
    {
        Task ExecuteFileDownloadAsync(FileDownloadTaskBase fileDownloadTask, CancellationToken cancellationToken);
    }
}