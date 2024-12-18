namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Hyperar.HUM.Application.Controllers;
    using Hyperar.HUM.UserInterface.Models;
    using Hyperar.HUM.UserInterface.State;
    using Hyperar.HUM.UserInterface.State.Interfaces;

    internal partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IMainMenuBuilderFactory mainMenuBuilderFactory;

        [ObservableProperty]
        private bool isMenuOpen;

        [ObservableProperty]
        private MenuItemTemplate? selectedItem;

        public MainWindowViewModel(
            INavigator navigator,
            IMainMenuBuilderFactory mainMenuBuilderFactory) : base(navigator)
        {
            this.mainMenuBuilderFactory = mainMenuBuilderFactory;
        }

        public bool CanNavigate
        {
            get
            {
                return this.navigator.CanNavigate;
            }
        }

        public ViewModelBase? CurrentViewModel
        {
            get
            {
                return this.navigator.CurrentViewModel;
            }
        }

        public ObservableCollection<MenuItemTemplate> MenuItems { get; private set; } = new ObservableCollection<MenuItemTemplate>();

        public override async Task InitializeAsync()
        {
            await this.BuildMenu();
        }

        private async Task BuildMenu()
        {
            var builder = await this.mainMenuBuilderFactory.GetBuilderAsync();

            var menuItems = await builder.GetMenuItemsAsync();
        }

        [RelayCommand]
        private void ToggleMenu()
        {
            this.IsMenuOpen = !this.IsMenuOpen;
        }
    }
}