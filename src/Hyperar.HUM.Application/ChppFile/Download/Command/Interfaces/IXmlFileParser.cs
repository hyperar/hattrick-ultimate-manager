namespace Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;

    public interface IXmlFileParser
    {
        Task<IXmlFileBase> ParseXmlFileAsync(byte[] fileContent, CancellationToken cancellationToken);
    }
}