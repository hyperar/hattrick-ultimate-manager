namespace Hyperar.HUM.UserInterface.State.Interfaces
{
    using System.Threading.Tasks;
    using Hyperar.HUM.Shared.Enums;

    internal interface ILandingViewFactory
    {
        Task<ViewType> GetLandingViewAsync();
    }
}