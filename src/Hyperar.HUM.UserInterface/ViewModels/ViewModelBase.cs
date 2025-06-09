namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.ComponentModel;
    using Hyperar.HUM.Application.UserProfile.Settings.Queries.Get.ByUserProfileId;
    using Hyperar.HUM.Shared.Models;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;
    using MediatR;

    internal abstract partial class ViewModelBase : ObservableObject
    {
        protected readonly INavigator Navigator;

        protected readonly ISender Sender;

        protected readonly ISessionStore SessionStore;

        [ObservableProperty]
        private bool isInitialized;

        protected ViewModelBase(
            INavigator navigator,
            ISessionStore sessionStore,
            ISender sender)
        {
            this.Navigator = navigator;
            this.SessionStore = sessionStore;
            this.Sender = sender;
        }

        protected UserProfileSettings? UserProfileSettings { get; set; }

        public virtual async Task InitializeAsync()
        {
            if (this.SessionStore.SelectedUserProfileId.HasValue)
            {
                await this.Sender.Send(
                    new GetUserProfileSettingsByUserProfileIdQuery
                    {
                        UserProfileId = this.SessionStore.SelectedUserProfileId.Value
                    });
            }

            this.IsInitialized = true;
        }
    }
}