namespace Hyperar.HUM.UserInterface.Command
{
    using System.Threading.Tasks;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.ViewModels.Interfaces;

    internal class UpdateCurrentViewModelCommand : AsyncCommandBase
    {
        private readonly INavigator navigator;

        private readonly IViewModelFactory viewModelFactory;

        public UpdateCurrentViewModelCommand(
            INavigator navigator,
            IViewModelFactory viewModelFactory)
        {
            this.navigator = navigator;
            this.viewModelFactory = viewModelFactory;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            this.navigator.SuspendNavigation();

            if (parameter is ViewType viewType)
            {
                this.navigator.SetCurrentViewModel(
                    await this.viewModelFactory.CreateViewModelAsync(
                        viewType));
            }
        }
    }
}