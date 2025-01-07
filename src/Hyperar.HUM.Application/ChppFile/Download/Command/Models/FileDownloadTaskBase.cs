namespace Hyperar.HUM.Application.ChppFile.Download.Command.Models
{
    using System;
    using Hyperar.HUM.Shared.Enums;

    public abstract class FileDownloadTaskBase
    {
        public string? ErrorMessage { get; set; }

        public FileType FileType { get; set; }

        public Guid Id { get; set; }

        public DownloadTaskStatus Status { get; set; }

        public string Title { get; set; } = string.Empty;
    }
}