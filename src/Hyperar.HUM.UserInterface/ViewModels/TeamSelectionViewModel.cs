namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Hyperar.HUM.Application.SeniorTeam.Queries.List.ByUserProfileId;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.TeamSelection;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;
    using MediatR;

    internal partial class TeamSelectionViewModel : ViewModelBase
    {
        private readonly ISender sender;

        [ObservableProperty]
        private ObservableCollection<SeniorTeam> seniorTeams;

        public TeamSelectionViewModel(
            INavigator navigator,
            ISessionStore sessionStore,
            ISender sender) : base(navigator, sessionStore)
        {
            this.sender = sender;

            this.SelectTeamCommand = new RelayCommand<long>(this.SelectTeam);
            this.SeniorTeams = new ObservableCollection<SeniorTeam>();
        }

        public RelayCommand<long> SelectTeamCommand { get; }

        public override async Task InitializeAsync()
        {
            ArgumentNullException.ThrowIfNull(this.SessionStore.SelectedUserProfileId);

            this.SeniorTeams = new ObservableCollection<SeniorTeam>(
                await this.sender.Send(
                    new ListSeniorTeamsByUserProfileIdQuery(
                        this.SessionStore.SelectedUserProfileId.Value)));

            this.Navigator.ResumeNavigation();

            await base.InitializeAsync();
        }

        private void SelectTeam(long hattrickId)
        {
            this.SessionStore.SetSelectedTeam(hattrickId);

            this.Navigator.SetTargetViewType(ViewType.Home);
        }
    }
}