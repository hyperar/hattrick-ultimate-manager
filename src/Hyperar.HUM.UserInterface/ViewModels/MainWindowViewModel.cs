namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Main;
    using Hyperar.HUM.UserInterface.Command;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;
    using Hyperar.HUM.UserInterface.ViewModels.Interfaces;

    internal partial class MainWindowViewModel : ViewModelBase, IDisposable
    {
        protected readonly AsyncCommandBase UpdateCurrentViewModelCommand;

        private readonly ViewType landingViewType;

        private readonly IMainMenuBuilderFactory mainMenuBuilderFactory;

        [ObservableProperty]
        private bool isMenuOpen;

        [ObservableProperty]
        private MenuItemTemplate? selectedItem;

        public MainWindowViewModel(
            INavigator navigator,
            ISessionStore sessionStore,
            IMainMenuBuilderFactory mainMenuBuilderFactory,
            IViewModelFactory viewModelFactory,
            ViewType landingViewType) : base(navigator, sessionStore)
        {
            this.mainMenuBuilderFactory = mainMenuBuilderFactory;
            this.landingViewType = landingViewType;

            this.Navigator.CanNavigateChanged += this.Navigator_CanNavigateChanged;
            this.Navigator.CurrentViewModelChanged += this.Navigator_CurrentViewModelChanged;
            this.Navigator.TargetViewTypeChanged += this.Navigator_TargetViewTypeChanged;
            this.ToggleMenuCommand = new RelayCommand(this.ToggleMenu);
            this.UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this.Navigator, viewModelFactory);
        }

        public bool CanNavigate
        {
            get
            {
                return this.Navigator.CanNavigate;
            }
        }

        public ViewModelBase? CurrentViewModel
        {
            get
            {
                return this.Navigator.CurrentViewModel;
            }
        }

        public ObservableCollection<MenuItemTemplate> MenuItems { get; private set; } = new ObservableCollection<MenuItemTemplate>();

        public RelayCommand ToggleMenuCommand { get; }

        public void Dispose()
        {
            this.Navigator.TargetViewTypeChanged -= this.Navigator_TargetViewTypeChanged;
            this.Navigator.CurrentViewModelChanged -= this.Navigator_CurrentViewModelChanged;
            this.Navigator.CanNavigateChanged -= this.Navigator_CanNavigateChanged;
        }

        public override async Task InitializeAsync()
        {
            this.Navigator.SuspendNavigation();

            await this.UpdateCurrentViewModelCommand.ExecuteAsync(this.landingViewType);

            await base.InitializeAsync();
        }

        private void Navigator_CanNavigateChanged()
        {
            this.OnPropertyChanged(nameof(this.CanNavigate));
        }

        private void Navigator_CurrentViewModelChanged()
        {
            this.OnPropertyChanged(nameof(this.CurrentViewModel));
        }

        private void Navigator_TargetViewTypeChanged()
        {
            var selectedItem = this.MenuItems.Where(x => x is MenuItemViewTemplate m
                                                  && m.ViewType == this.Navigator.TargetViewType)
                                             .SingleOrDefault();

            if (selectedItem != null)
            {
                this.SelectedItem = selectedItem;
            }
            else
            {
                this.UpdateCurrentViewModelCommand.Execute(this.Navigator.TargetViewType);
            }
        }

        private void ToggleMenu()
        {
            this.IsMenuOpen = !this.IsMenuOpen;
        }
    }
}