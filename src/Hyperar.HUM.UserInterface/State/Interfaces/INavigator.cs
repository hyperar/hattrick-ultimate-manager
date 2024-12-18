namespace Hyperar.HUM.UserInterface.State.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using Hyperar.HUM.UserInterface.ViewModels;

    internal interface INavigator
    {
        event Action? CanNavigateChanged;

        event Action? CurrentViewModelChanged;

        event Func<Task>? SelectedTeamChanged;

        event Func<Task>? SelectedUserProfileChanged;

        bool CanNavigate { get; }

        ViewModelBase? CurrentViewModel { get; }

        long? SelectedTeamHattrickId { get; }

        Guid? SelectedUserProfileId { get; }

        Task ResumeNavigationAsync();

        Task SetSelectedTeamAsync(long selectedTeamHattrickId);

        Task SuspendNavigationAsync();
    }
}