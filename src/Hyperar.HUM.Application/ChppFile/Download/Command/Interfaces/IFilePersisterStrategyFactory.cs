namespace Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces
{
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;

    public interface IFilePersisterStrategyFactory
    {
        IFilePersisterStrategy GetFor(FileDownloadTaskBase fileDownloadTask);
    }
}