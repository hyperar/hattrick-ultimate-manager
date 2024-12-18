namespace Hyperar.HUM.UserInterface.Models
{
    internal abstract class MenuItemTemplate
    {
        protected bool IsEnabled { get; set; }

        protected bool IsSelectable { get; set; }
    }
}