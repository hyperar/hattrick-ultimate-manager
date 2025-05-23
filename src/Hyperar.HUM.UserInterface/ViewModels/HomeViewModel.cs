namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System.Threading.Tasks;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;

    internal class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(
            INavigator navigator,
            ISessionStore sessionStore) : base(navigator, sessionStore)
        {
        }

        public override async Task InitializeAsync()
        {
            this.Navigator.ResumeNavigation();

            await base.InitializeAsync();
        }
    }
}