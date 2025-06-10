namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Avalonia.Controls.Selection;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Main;
    using Hyperar.HUM.UserInterface.Command;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.ViewModels.Interfaces;

    internal partial class MainWindowViewModel : ObservableObject, IDisposable
    {
        protected readonly AsyncCommandBase UpdateCurrentViewModelCommand;

        private readonly ViewType landingViewType;

        private readonly INavigator navigator;

        [ObservableProperty]
        private bool isMenuOpen;

        public MainWindowViewModel(
            INavigator navigator,
            IViewModelFactory viewModelFactory,
            ViewType landingViewType)
        {
            this.landingViewType = landingViewType;

            this.navigator = navigator;
            this.navigator.CanNavigateChanged += this.Navigator_CanNavigateChanged;
            this.navigator.CurrentViewModelChanged += this.Navigator_CurrentViewModelChanged;
            this.navigator.TargetViewTypeChanged += this.Navigator_TargetViewTypeChanged;

            this.SelectionModel = new SelectionModel<MenuItemTemplate?>();
            this.SelectionModel.SelectionChanged += new EventHandler<SelectionModelSelectionChangedEventArgs>(this.ListBox_OnSelectionChanged);

            this.ToggleMenuCommand = new RelayCommand(this.ToggleMenu);
            this.UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this.navigator, viewModelFactory);

            this.MenuItems = new ObservableCollection<MenuItemTemplate>
            {
                new MenuItemView(Globalization.Controls.Home, true, ViewType.Home, "HomeIcon"),
                new MenuItemSeparator(),
                new MenuItemView(Globalization.Controls.Download, true, ViewType.Download, "DownloadIcon"),
                new MenuItemView(Globalization.Controls.TeamSelection, true, ViewType.TeamSelection, "TeamSelectionIcon"),
                new MenuItemView(Globalization.Controls.Authorization, true, ViewType.Authorization, "AuthorizationIcon"),
                new MenuItemView(Globalization.Controls.UserProfiles, true, ViewType.UserProfileSelection, "UserProfilesIcon"),
            };
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

        public ISelectionModel SelectionModel { get; }

        public RelayCommand ToggleMenuCommand { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task InitializeAsync()
        {
            this.navigator.SuspendNavigation();

            await this.UpdateCurrentViewModelCommand.ExecuteAsync(this.landingViewType);

            this.navigator.ResumeNavigation();
        }

        protected virtual void Dispose(bool disposing)
        {
            this.navigator.TargetViewTypeChanged -= this.Navigator_TargetViewTypeChanged;
            this.navigator.CurrentViewModelChanged -= this.Navigator_CurrentViewModelChanged;
            this.navigator.CanNavigateChanged -= this.Navigator_CanNavigateChanged;
        }

        private void ListBox_OnSelectionChanged(object? sender, SelectionModelSelectionChangedEventArgs eventArgs)
        {
            if (eventArgs.SelectedItems.SingleOrDefault() is MenuItemView menuItemView)
            {
                this.UpdateCurrentViewModelCommand.Execute(
                    this.MenuItems.OfType<MenuItemView>()
                        .Where(x => x.ViewType == menuItemView.ViewType)
                        .Select(x => x.ViewType)
                        .Single());
            }
        }

        private void Navigator_CanNavigateChanged()
        {
            this.OnPropertyChanged(nameof(this.CanNavigate));
        }

        private void Navigator_CurrentViewModelChanged()
        {
            this.OnPropertyChanged(nameof(this.CurrentViewModel));
        }

        private void Navigator_TargetViewTypeChanged(ViewType viewType)
        {
            this.SelectionModel.SelectedItem = this.MenuItems.OfType<MenuItemView>()
                .SingleOrDefault(x => x.ViewType == viewType);
        }

        private void ToggleMenu()
        {
            this.IsMenuOpen = !this.IsMenuOpen;
        }
    }
}