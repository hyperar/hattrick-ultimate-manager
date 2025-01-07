namespace Hyperar.HUM.Shared.Models.Download
{
    using System.Collections.Generic;

    public record DownloadReport(
        IEnumerable<DownloadTask> DownloadTasks,
        DownloadTask? CurrentTask,
        int CompletedTasks,
        int Tasks,
        bool IsDownloading);
}