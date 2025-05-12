namespace Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;

    public interface IFileParseStrategy
    {
        Task<IXmlFileBase> ExecuteFileParseAsync(
            XmlReader xmlReader,
            string fileName,
            decimal version,
            long userId,
            DateTime fetchedDate,
            CancellationToken cancellationToken);
    }
}