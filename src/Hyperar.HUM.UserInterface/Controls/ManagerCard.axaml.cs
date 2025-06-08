namespace Hyperar.HUM.UserInterface.Controls
{
    using Avalonia;
    using Avalonia.Controls.Primitives;

    public class ManagerCard : TemplatedControl
    {
        public static readonly StyledProperty<byte[]?> AvatarBytesProperty = AvaloniaProperty.Register<ManagerCard, byte[]?>(nameof(AvatarBytes));

        public static readonly StyledProperty<byte[]> CountryFlagBytesProperty = AvaloniaProperty.Register<ManagerCard, byte[]>(nameof(CountryFlagBytes));

        public static readonly StyledProperty<long> CountryHattrickIdProperty = AvaloniaProperty.Register<ManagerCard, long>(nameof(CountryHattrickId));

        public static readonly StyledProperty<string> CountryNameProperty = AvaloniaProperty.Register<ManagerCard, string>(nameof(CountryName));

        public static readonly StyledProperty<long> ManagerHattrickIdProperty = AvaloniaProperty.Register<ManagerCard, long>(nameof(ManagerHattrickId));

        public static readonly StyledProperty<string> ManagerNameProperty = AvaloniaProperty.Register<ManagerCard, string>(nameof(ManagerName));

        public byte[]? AvatarBytes
        {
            get { return this.GetValue(AvatarBytesProperty); }
            set { this.SetValue(AvatarBytesProperty, value); }
        }

        public byte[] CountryFlagBytes
        {
            get { return this.GetValue(CountryFlagBytesProperty); }
            set { this.SetValue(CountryFlagBytesProperty, value); }
        }

        public long CountryHattrickId
        {
            get { return this.GetValue(CountryHattrickIdProperty); }
            set { this.SetValue(CountryHattrickIdProperty, value); }
        }

        public string CountryName
        {
            get { return this.GetValue(CountryNameProperty); }
            set { this.SetValue(CountryNameProperty, value); }
        }

        public long ManagerHattrickId
        {
            get { return this.GetValue(ManagerHattrickIdProperty); }
            set { this.SetValue(ManagerHattrickIdProperty, value); }
        }

        public string ManagerName
        {
            get { return this.GetValue(ManagerNameProperty); }
            set { this.SetValue(ManagerNameProperty, value); }
        }
    }
}