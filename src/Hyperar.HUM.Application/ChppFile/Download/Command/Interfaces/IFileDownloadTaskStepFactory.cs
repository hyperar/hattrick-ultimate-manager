namespace Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces
{
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;

    public interface IFileDownloadTaskStepFactory
    {
        IFileDownloadTaskStep GetNextStep(FileDownloadTaskBase fileDownloadTask);
    }
}