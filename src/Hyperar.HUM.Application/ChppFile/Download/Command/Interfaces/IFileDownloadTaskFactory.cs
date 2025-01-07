namespace Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces
{
    using System;
    using System.Collections.Specialized;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.Shared.Enums;

    public interface IFileDownloadTaskFactory
    {
        ImageFileDownloadTask BuildImageFileDownloadTask(
            string url);

        XmlFileDownloadTask BuildXmlFileDownloadTask(
                    Guid UserProfileId,
                    XmlFileType fileType,
                    NameValueCollection? parameters = null);
    }
}