namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;

    public class EmptyPersister : IFilePersisterStrategy
    {
        public Task PersistFileAsync(FileDownloadTaskBase fileDownloadTask, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}