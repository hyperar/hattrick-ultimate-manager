namespace Hyperar.HUM.Application.ChppFile.Download.Command.Models
{
    using System.Collections.Specialized;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;

    public class XmlFileDownloadTask : FileDownloadTaskBase
    {
        public IXmlFileBase? Entity { get; set; }

        public byte[]? FileContent { get; set; }

        public NameValueCollection? Parameters { get; set; }

        public Guid UserProfileId { get; set; }

        public XmlFileType XmlFile { get; set; }
    }
}