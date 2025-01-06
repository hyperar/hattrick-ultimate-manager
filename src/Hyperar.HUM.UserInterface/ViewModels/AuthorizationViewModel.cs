namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.CheckToken;
    using Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.DeleteToken;
    using Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.RevokeToken;
    using Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.SaveToken;
    using Hyperar.HUM.Application.OAuthToken.AccessToken.Queries.Get.ByUserProfileId;
    using Hyperar.HUM.Application.OAuthToken.AccessToken.Queries.Get.FromHattrick;
    using Hyperar.HUM.Application.OAuthToken.AuthorizationUrl.Queries.Get.ForRequestToken;
    using Hyperar.HUM.Application.OAuthToken.RequestToken.Queries.Get.FromHattrick;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Authorization;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;
    using MediatR;

    internal partial class AuthorizationViewModel : ViewModelBase
    {
        private readonly ISender sender;

        [ObservableProperty]
        private bool accessTokenVerified;

        private string? authorizationUrl;

        [ObservableProperty]
        private bool canCheckAccessToken;

        [ObservableProperty]
        private bool canRevokeAccessToken;

        private RequestToken? requestToken;

        [ObservableProperty]
        private UserProfileToken? userProfileToken;

        private string? verificationCode;

        public AuthorizationViewModel(
            INavigator navigator,
            ISessionStore sessionStore,
            ISender sender) : base(navigator, sessionStore)
        {
            this.sender = sender;

            this.CheckAccessTokenCommand = new AsyncRelayCommand(this.CheckAccessTokenAsync);
            this.GetAccessTokenCommand = new AsyncRelayCommand(this.GetAccessTokenAsync);
            this.RevokeAccessTokenCommand = new AsyncRelayCommand(this.RevokeAccessTokenAsync);
            this.OpenAuthorizationWebPageCommand = new AsyncRelayCommand(this.OpenAuthorizationWebPage);
        }

        public bool CanGetAccessToken
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.VerificationCode)
                    && this.requestToken is not null
                    && !string.IsNullOrWhiteSpace(this.requestToken.Token)
                    && !string.IsNullOrWhiteSpace(this.requestToken.Secret);
            }
        }

        public AsyncRelayCommand CheckAccessTokenCommand { get; }

        public AsyncRelayCommand GetAccessTokenCommand { get; }

        public AsyncRelayCommand OpenAuthorizationWebPageCommand { get; }

        public AsyncRelayCommand RevokeAccessTokenCommand { get; }

        public string? VerificationCode
        {
            get
            {
                return this.verificationCode;
            }

            set
            {
                this.verificationCode = value;

                this.OnPropertyChanged(nameof(this.VerificationCode));
                this.OnPropertyChanged(nameof(this.CanGetAccessToken));
            }
        }

        public override async Task InitializeAsync()
        {
            ArgumentNullException.ThrowIfNull(this.SessionStore.SelectedUserProfileId);

            this.UserProfileToken = await this.sender.Send(
                new GetAccessTokenByUserProfileIdQuery(
                    this.SessionStore.SelectedUserProfileId.Value));

            this.CanCheckAccessToken =
            this.CanRevokeAccessToken = this.UserProfileToken is not null;

            await base.InitializeAsync();
        }

        private async Task CheckAccessTokenAsync()
        {
            ArgumentNullException.ThrowIfNull(this.UserProfileToken);

            this.AccessTokenVerified = await this.sender.Send(
                new CheckAccessTokenCommand(
                    this.UserProfileToken));

            if (!this.AccessTokenVerified)
            {
                await this.DeleteAccessTokenAsync();
            }
        }

        private async Task DeleteAccessTokenAsync()
        {
            ArgumentNullException.ThrowIfNull(this.UserProfileToken);

            await this.sender.Send(
                new DeleteTokenCommand(
                    this.UserProfileToken.Id));

            this.CanCheckAccessToken =
            this.CanRevokeAccessToken = false;

            this.UserProfileToken = null;
        }

        private async Task GetAccessTokenAsync()
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(this.verificationCode);
            ArgumentNullException.ThrowIfNull(this.requestToken);

            var accessToken = await this.sender.Send(
                new GetAccessTokenFromHattrickQuery(
                    this.verificationCode,
                    this.requestToken));

            ArgumentNullException.ThrowIfNull(this.SessionStore.SelectedUserProfileId);

            await this.sender.Send(
                new SaveTokenCommand(
                    this.SessionStore.SelectedUserProfileId.Value,
                    accessToken));

            this.authorizationUrl = null;
            this.requestToken = null;
            this.VerificationCode = null;

            this.CanRevokeAccessToken =
            this.CanCheckAccessToken = true;

            this.OnPropertyChanged(nameof(this.CanGetAccessToken));

            ArgumentNullException.ThrowIfNull(this.SessionStore.SelectedUserProfileId);

            this.UserProfileToken = await this.sender.Send(
                new GetAccessTokenByUserProfileIdQuery(
                    this.SessionStore.SelectedUserProfileId.Value));

            this.Navigator.SetTargetViewType(ViewType.Download);
        }

        private async Task OpenAuthorizationWebPage()
        {
            this.requestToken = await this.sender.Send(
                new GetRequestTokenFromHattrickQuery());

            this.authorizationUrl = await this.sender.Send(
                new GetAuthorizationUrlForRequestTokenQuery(this.requestToken));

            Process.Start(
                new ProcessStartInfo(
                    this.authorizationUrl)
                {
                    UseShellExecute = true
                });
        }

        private async Task RevokeAccessTokenAsync()
        {
            ArgumentNullException.ThrowIfNull(this.UserProfileToken);

            await this.sender.Send(
                new RevokeTokenCommand(
                    this.UserProfileToken.Token,
                    this.UserProfileToken.Secret,
                    this.UserProfileToken.CreatedOn,
                    this.UserProfileToken.ExpiresOn));

            await this.DeleteAccessTokenAsync();
        }
    }
}