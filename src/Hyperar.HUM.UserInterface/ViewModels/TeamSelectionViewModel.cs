namespace Hyperar.HUM.UserInterface.ViewModels
{
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;

    internal class TeamSelectionViewModel : ViewModelBase
    {
        public TeamSelectionViewModel(INavigator navigator, ISessionStore sessionStore) : base(navigator, sessionStore)
        {
        }
    }
}