namespace Hyperar.HUM.UserInterface.Models
{
    using System;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Media;
    using Hyperar.HUM.UserInterface.State.Enums;

    internal class MenuItemViewTemplate : MenuItemTemplate
    {
        private const string MissingIconKey = "MissingIcon";

        public MenuItemViewTemplate(string text, bool isEnabled, ViewType viewType) : this(text, isEnabled, viewType, MissingIconKey)
        {
        }

        public MenuItemViewTemplate(string text, bool isEnabled, ViewType viewType, string iconResourceKey)
        {
            this.IsSelectable = true;
            this.Text = text;
            this.ViewType = viewType;

            object? resource = Application.Current?.FindResource(iconResourceKey) ?? Application.Current?.FindResource(MissingIconKey);

            ArgumentNullException.ThrowIfNull(resource);

            this.Icon = resource is StreamGeometry icon ? icon : throw new InvalidCastException(nameof(resource));
        }

        public StreamGeometry Icon { get; }

        public string Text { get; }

        public ViewType ViewType { get; }
    }
}