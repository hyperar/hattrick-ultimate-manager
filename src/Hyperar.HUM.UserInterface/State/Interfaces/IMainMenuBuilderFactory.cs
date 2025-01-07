namespace Hyperar.HUM.UserInterface.State.Interfaces
{
    internal interface IMainMenuBuilderFactory
    {
        IMainMenuBuilderStrategy GetMainMenuBuilderAsync();
    }
}