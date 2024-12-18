namespace Hyperar.HUM.UserInterface.ViewModels.Interfaces
{
    using System.Threading.Tasks;
    using Hyperar.HUM.UserInterface.State.Enums;

    internal interface IViewModelFactory
    {
        Task<ViewModelBase> CreateViewModelAsync(ViewType viewType);
    }
}