namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Hyperar.HUM.Application.SeniorTeam.Queries.List.ByUserProfileId;
    using Hyperar.HUM.Application.UserProfile.Commands.UpdateSelectedTeam;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.TeamSelection;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;
    using MediatR;

    internal partial class TeamSelectionViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<SeniorTeam> seniorTeams;

        public TeamSelectionViewModel(
            INavigator navigator,
            ISessionStore sessionStore,
            ISender sender) : base(navigator, sessionStore, sender)
        {
            this.SelectTeamCommand = new AsyncRelayCommand<long>(this.SelectTeam);
            this.SeniorTeams = new ObservableCollection<SeniorTeam>();
        }

        public ICommand SelectTeamCommand { get; }

        public override async Task InitializeAsync()
        {
            ArgumentNullException.ThrowIfNull(this.SessionStore.SelectedUserProfileId);

            this.SeniorTeams = new ObservableCollection<SeniorTeam>(
                await this.Sender.Send(
                    new ListSeniorTeamsByUserProfileIdQuery(
                        this.SessionStore.SelectedUserProfileId.Value)));

            this.Navigator.ResumeNavigation();

            await base.InitializeAsync();
        }

        private async Task SelectTeam(long hattrickId)
        {
            this.SessionStore.SetSelectedTeam(hattrickId);

            ArgumentNullException.ThrowIfNull(this.SessionStore.SelectedUserProfileId);
            ArgumentNullException.ThrowIfNull(this.SessionStore.SelectedTeamHattrickId);

            await this.Sender.Send(
                new UpdateUserProfileSelectedTeamCommand
                {
                    UserProfileId = this.SessionStore.SelectedUserProfileId.Value,
                    SelectedTeamHattrickId = this.SessionStore.SelectedTeamHattrickId.Value
                });

            this.Navigator.SetTargetViewType(ViewType.Home);
        }
    }
}