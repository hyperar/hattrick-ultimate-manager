namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System.Threading.Tasks;
    using Hyperar.HUM.UserInterface.State.Interfaces;

    internal class HomeViewModel : ViewModelBase
    {
        public HomeViewModel(INavigator navigator) : base(navigator)
        {
        }

        public override async Task InitializeAsync()
        {
            await Task.Delay(1000);

            await base.InitializeAsync();
        }
    }
}