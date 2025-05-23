namespace Hyperar.HUM.UserInterface.State
{
    using System;
    using System.Threading.Tasks;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.ViewModels;

    internal class Navigator : INavigator
    {
        private bool canNavigate;

        private ViewModelBase? currentViewModel;

        private long? selectedTeamHattrickId;

        public event Action? CanNavigateChanged;

        public event Action? CurrentViewModelChanged;

        public event Func<Task>? SelectedTeamChanged;

        public event Action<ViewType>? TargetViewTypeChanged;

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

        public void ResumeNavigation()
        {
            this.CanNavigate = true;
        }

        public void SetCurrentViewModel(ViewModelBase viewModel)
        {
            this.CurrentViewModel = viewModel;
        }

        public void SetTargetViewType(ViewType viewType)
        {
            this.TargetViewTypeChanged?.Invoke(viewType);
        }

        public void SuspendNavigation()
        {
            this.CanNavigate = false;
        }
    }
}