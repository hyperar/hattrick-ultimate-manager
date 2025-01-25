namespace Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces
{
    using Hyperar.HUM.Shared.Enums;

    public interface IFileDownloadTaskStepFactory
    {
        IFileDownloadTaskStep GetNextStep(DownloadTaskStatus status);
    }
}