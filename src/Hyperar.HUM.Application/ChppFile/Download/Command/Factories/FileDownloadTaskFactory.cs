namespace Hyperar.HUM.Application.ChppFile.Download.Command.Factories
{
    using System;
    using System.Collections.Specialized;
    using Hyperar.HUM.Application;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Shared.Enums;

    public class FileDownloadTaskFactory : IFileDownloadTaskFactory
    {
        public ImageFileDownloadTask BuildImageFileDownloadTask(string url)
        {
            return new ImageFileDownloadTask
            {
                Id = Guid.NewGuid(),
                Title = ImageHelper.NormalizeUrl(url),
                Url = ImageHelper.NormalizeUrl(url),
                FileType = FileType.ImageFile,
                Status = DownloadTaskStatus.Pending
            };
        }

        public XmlFileDownloadTask BuildXmlFileDownloadTask(
            Guid UserProfileId,
            XmlFileType fileType,
            NameValueCollection? parameters = null)
        {
            return new XmlFileDownloadTask
            {
                Id = Guid.NewGuid(),
                UserProfileId = UserProfileId,
                Title = Globalization.Enums.ResourceManager.GetString($"{fileType.GetType().Name}_{fileType}")
                    ?? fileType.ToString(),
                FileType = FileType.XmlFile,
                Status = DownloadTaskStatus.Pending,
                XmlFile = fileType,
                Parameters = parameters
            };
        }
    }
}