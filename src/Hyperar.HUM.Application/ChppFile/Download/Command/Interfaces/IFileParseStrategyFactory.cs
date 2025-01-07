namespace Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces
{
    public interface IFileParseStrategyFactory
    {
        IFileParseStrategy GetFor(string fileName);
    }
}