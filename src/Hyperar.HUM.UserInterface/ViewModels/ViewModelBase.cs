namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.ComponentModel;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;

    internal abstract partial class ViewModelBase : ObservableObject
    {
        protected readonly INavigator Navigator;

        protected readonly ISessionStore SessionStore;

        [ObservableProperty]
        private bool isInitialized;

        protected ViewModelBase(
            INavigator navigator,
            ISessionStore sessionStore)
        {
            this.Navigator = navigator;
            this.SessionStore = sessionStore;
        }

        public virtual Task InitializeAsync()
        {
            this.IsInitialized = true;

            return Task.CompletedTask;
        }
    }
}