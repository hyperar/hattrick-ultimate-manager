namespace Hyperar.HUM.UserInterface.State.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Hyperar.HUM.UserInterface.Models;

    internal interface IMainMenuBuilderStrategy
    {
        Task<ICollection<MenuItemTemplate>> GetMenuItemsAsync();
    }
}