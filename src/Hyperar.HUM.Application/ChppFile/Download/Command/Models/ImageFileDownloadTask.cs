namespace Hyperar.HUM.Application.ChppFile.Download.Command.Models
{
    public class ImageFileDownloadTask : FileDownloadTaskBase
    {
        public byte[]? ImageFileBytes { get; set; }

        public string Url { get; set; } = string.Empty;
    }
}