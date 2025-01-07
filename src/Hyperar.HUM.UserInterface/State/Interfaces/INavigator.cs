namespace Hyperar.HUM.UserInterface.State.Interfaces
{
    using System;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.UserInterface.ViewModels;

    internal interface INavigator
    {
        event Action? CanNavigateChanged;

        event Action? CurrentViewModelChanged;

        event Action? TargetViewTypeChanged;

        bool CanNavigate { get; }

        ViewModelBase? CurrentViewModel { get; }

        ViewType? TargetViewType { get; }

        void ResumeNavigation();

        void SetCurrentViewModel(ViewModelBase viewModel);

        void SetTargetViewType(ViewType viewType);

        void SuspendNavigation();
    }
}