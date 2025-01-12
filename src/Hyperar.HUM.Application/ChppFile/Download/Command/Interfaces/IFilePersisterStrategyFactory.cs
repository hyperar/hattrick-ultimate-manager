namespace Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces
{
    using Hyperar.HUM.Shared.Enums;

    public interface IFilePersisterStrategyFactory
    {
        IFilePersisterStrategy GetFor(XmlFileType xmlFile);
    }
}