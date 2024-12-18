namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.ComponentModel;
    using Hyperar.HUM.UserInterface.State.Interfaces;

    internal abstract partial class ViewModelBase : ObservableObject
    {
        protected readonly INavigator navigator;

        [ObservableProperty]
        private bool isInitialized;

        protected ViewModelBase(INavigator navigator)
        {
            this.navigator = navigator;
        }

        public virtual Task InitializeAsync()
        {
            this.IsInitialized = true;

            return Task.CompletedTask;
        }
    }
}