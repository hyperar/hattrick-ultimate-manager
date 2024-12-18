namespace Hyperar.HUM.UserInterface.State.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal interface IMainMenuBuilderFactory
    {
        Task<IMainMenuBuilderStrategy> GetBuilderAsync();
    }
}