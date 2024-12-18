namespace Hyperar.HUM.UserInterface.State
{
    using System;
    using System.Threading.Tasks;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.ViewModels;

    internal class Navigator : INavigator
    {
        private bool canNavigate;

        private ViewModelBase? currentViewModel;

        private long? selectedTeamHattrickId;

        private Guid? selectedUserProfileId;

        public event Action? CanNavigateChanged;

        public event Action? CurrentViewModelChanged;

        public event Func<Task>? SelectedTeamChanged;

        public event Func<Task>? SelectedUserProfileChanged;

        public bool CanNavigate
        {
            get
            {
                return this.canNavigate;
            }

            private set
            {
                this.canNavigate = value;

                this.CanNavigateChanged?.Invoke();
            }
        }

        public ViewModelBase? CurrentViewModel
        {
            get
            {
                return this.currentViewModel;
            }

            private set
            {
                this.currentViewModel = value;

                this.CurrentViewModelChanged?.Invoke();
            }
        }

        public long? SelectedTeamHattrickId
        {
            get
            {
                return this.selectedTeamHattrickId;
            }

            private set
            {
                this.selectedTeamHattrickId = value;

                this.SelectedTeamChanged?.Invoke();
            }
        }

        public Guid? SelectedUserProfileId
        {
            get
            {
                return this.selectedUserProfileId;
            }

            private set
            {
                this.selectedUserProfileId = value;

                this.SelectedUserProfileChanged?.Invoke();
            }
        }

        public Task ResumeNavigationAsync()
        {
            this.CanNavigate = true;

            return Task.CompletedTask;
        }

        public Task SetSelectedTeamAsync(long selectedTeamHattrickId)
        {
            this.SelectedTeamHattrickId = selectedTeamHattrickId;

            return Task.CompletedTask;
        }

        public Task SuspendNavigationAsync()
        {
            this.CanNavigate = false;

            return Task.CompletedTask;
        }
    }
}