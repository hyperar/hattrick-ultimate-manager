namespace Hyperar.HUM.Shared.Models.Download
{
    using System;
    using Hyperar.HUM.Shared.Enums;

    public record DownloadTask(Guid Id, FileType FileType, string Title, DownloadTaskStatus Status, string? ErrorMessage)
    {
        public bool HasError { get { return this.Status == DownloadTaskStatus.Error; } }
    }
}