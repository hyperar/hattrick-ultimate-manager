namespace Hyperar.HUM.UserInterface.State.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hyperar.HUM.Shared.Models.Main;

    internal interface IMainMenuBuilderStrategy
    {
        Task<ICollection<MenuItemTemplate>> GetMenuItemsAsync();
    }
}