namespace Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;

    public interface IFileDownloadTaskStep
    {
        Task ExecuteAsync(
            FileDownloadTaskBase fileDownloadTask,
            List<FileDownloadTaskBase> fileDownloadTasks,
            CancellationToken cancellationToken);
    }
}