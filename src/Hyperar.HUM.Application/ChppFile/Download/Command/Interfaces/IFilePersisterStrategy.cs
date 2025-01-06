namespace Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces
{
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;

    public interface IFilePersisterStrategy
    {
        Task PersistFileAsync(FileDownloadTaskBase fileDownloadTask, CancellationToken cancellationToken);
    }
}