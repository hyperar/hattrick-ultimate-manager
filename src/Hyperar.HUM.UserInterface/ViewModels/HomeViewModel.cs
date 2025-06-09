namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System.Threading.Tasks;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;
    using MediatR;

    internal class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(
            INavigator navigator,
            ISessionStore sessionStore,
            ISender sender) : base(navigator, sessionStore, sender)
        {
        }

        public override async Task InitializeAsync()
        {
            this.Navigator.ResumeNavigation();

            await base.InitializeAsync();
        }
    }
}