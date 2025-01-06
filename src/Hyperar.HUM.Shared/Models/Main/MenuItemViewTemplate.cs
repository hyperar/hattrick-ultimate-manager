namespace Hyperar.HUM.Shared.Models.Main
{
    using System;
    using Avalonia;
    using Avalonia.Controls;
    using Avalonia.Media;
    using Hyperar.HUM.Shared.Enums;

    public class MenuItemViewTemplate : MenuItemTemplate
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

            var resource = Application.Current?.FindResource(iconResourceKey) ?? Application.Current?.FindResource(MissingIconKey);

            ArgumentNullException.ThrowIfNull(resource);

            this.Icon = resource is StreamGeometry icon ? icon : throw new InvalidCastException(nameof(resource));
        }

        public StreamGeometry Icon { get; }

        public string Text { get; }

        public ViewType ViewType { get; }
    }
}