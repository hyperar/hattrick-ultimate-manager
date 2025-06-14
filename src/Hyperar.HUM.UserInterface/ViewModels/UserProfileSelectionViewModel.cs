﻿namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Hyperar.HUM.Application.UserProfile.Commands.Create;
    using Hyperar.HUM.Application.UserProfile.Queries.Get.ById;
    using Hyperar.HUM.Application.UserProfile.Queries.List;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.UserProfileSelection;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;
    using MediatR;

    internal partial class UserProfileSelectionViewModel : ViewModelBase
    {
        private readonly ISender sender;

        [ObservableProperty]
        private ObservableCollection<UserProfile> userProfiles = new ObservableCollection<UserProfile>();

        public UserProfileSelectionViewModel(
            INavigator navigator,
            ISessionStore sessionStore,
            ISender sender) : base(navigator, sessionStore)
        {
            this.sender = sender;

            this.CreateUserProfileCommand = new AsyncRelayCommand(this.CreateUserProfileAsync);
            this.SelectUserProfileCommand = new AsyncRelayCommand<Guid>(this.SelectUserProfileAsync);
        }

        public bool CanCreateUserProfile
        {
            get
            {
                return this.UserProfiles.All(x => x.Manager != null);
            }
        }

        public AsyncRelayCommand CreateUserProfileCommand { get; }

        public AsyncRelayCommand<Guid> SelectUserProfileCommand { get; }

        public bool ShowUserProfileSelector
        {
            get
            {
                return this.UserProfiles.Count > 0;
            }
        }

        public override async Task InitializeAsync()
        {
            await this.RefreshUserProfiles();

            this.Navigator.ResumeNavigation();

            await base.InitializeAsync();
        }

        private async Task CreateUserProfileAsync()
        {
            var userProfile = await this.sender.Send(
                new CreateUserProfileCommand());

            await this.SelectUserProfileAsync(userProfile.Id);
        }

        private async Task RefreshUserProfiles()
        {
            var userProfiles = await this.sender.Send(
                new ListUserProfilesQuery());

            this.UserProfiles = new ObservableCollection<UserProfile>(userProfiles);

            this.OnPropertyChanged(nameof(this.CanCreateUserProfile));
            this.OnPropertyChanged(nameof(this.ShowUserProfileSelector));
        }

        private async Task SelectUserProfileAsync(Guid userProfileId)
        {
            this.SessionStore.SetSelectedUserProfile(userProfileId);

            var userProfile = await this.sender.Send(
                new GetUserProfileByIdQuery(
                    userProfileId));

            if (userProfile.HasAuthorized)
            {
                this.Navigator.SetTargetViewType(ViewType.TeamSelection);
            }
            else
            {
                this.Navigator.SetTargetViewType(ViewType.Authorization);
            }
        }
    }
}