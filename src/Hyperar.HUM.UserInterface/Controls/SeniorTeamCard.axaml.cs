namespace Hyperar.HUM.UserInterface.Controls
{
    using Avalonia;
    using Avalonia.Controls.Primitives;

    public class SeniorTeamCard : TemplatedControl
    {
        public static readonly StyledProperty<byte[]> CountryFlagBytesProperty = AvaloniaProperty.Register<SeniorTeamCard, byte[]>(nameof(CountryFlagBytes));

        public static readonly StyledProperty<long> CountryHattrickIdProperty = AvaloniaProperty.Register<SeniorTeamCard, long>(nameof(CountryHattrickId));

        public static readonly StyledProperty<string> CountryNameProperty = AvaloniaProperty.Register<SeniorTeamCard, string>(nameof(CountryName));

        public static readonly StyledProperty<byte[]> LeagueFlagBytesProperty = AvaloniaProperty.Register<SeniorTeamCard, byte[]>(nameof(LeagueFlagBytes));

        public static readonly StyledProperty<long> LeagueHattrickIdProperty = AvaloniaProperty.Register<SeniorTeamCard, long>(nameof(LeagueHattrickId));

        public static readonly StyledProperty<string> LeagueNameProperty = AvaloniaProperty.Register<SeniorTeamCard, string>(nameof(LeagueName));

        public static readonly StyledProperty<long> RegionHattrickIdProperty = AvaloniaProperty.Register<SeniorTeamCard, long>(nameof(RegionHattrickId));

        public static readonly StyledProperty<string> RegionNameProperty = AvaloniaProperty.Register<SeniorTeamCard, string>(nameof(RegionName));

        public static readonly StyledProperty<long> SeriesHattrickIdProperty = AvaloniaProperty.Register<SeniorTeamCard, long>(nameof(SeriesHattrickId));

        public static readonly StyledProperty<string> SeriesNameProperty = AvaloniaProperty.Register<SeniorTeamCard, string>(nameof(SeriesName));

        public static readonly StyledProperty<long> TeamHattrickIdProperty = AvaloniaProperty.Register<SeniorTeamCard, long>(nameof(TeamHattrickId));

        public static readonly StyledProperty<byte[]?> TeamLogoBytesProperty = AvaloniaProperty.Register<SeniorTeamCard, byte[]?>(nameof(TeamLogoBytes));

        public static readonly StyledProperty<string> TeamNameProperty = AvaloniaProperty.Register<SeniorTeamCard, string>(nameof(TeamName));

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

        public byte[] LeagueFlagBytes
        {
            get { return this.GetValue(LeagueFlagBytesProperty); }
            set { this.SetValue(LeagueFlagBytesProperty, value); }
        }

        public long LeagueHattrickId
        {
            get { return this.GetValue(LeagueHattrickIdProperty); }
            set { this.SetValue(LeagueHattrickIdProperty, value); }
        }

        public string LeagueName
        {
            get { return this.GetValue(LeagueNameProperty); }
            set { this.SetValue(LeagueNameProperty, value); }
        }

        public long RegionHattrickId
        {
            get { return this.GetValue(RegionHattrickIdProperty); }
            set { this.SetValue(RegionHattrickIdProperty, value); }
        }

        public string RegionName
        {
            get { return this.GetValue(RegionNameProperty); }
            set { this.SetValue(RegionNameProperty, value); }
        }

        public long SeriesHattrickId
        {
            get { return this.GetValue(SeriesHattrickIdProperty); }
            set { this.SetValue(SeriesHattrickIdProperty, value); }
        }

        public string SeriesName
        {
            get { return this.GetValue(SeriesNameProperty); }
            set { this.SetValue(SeriesNameProperty, value); }
        }

        public long TeamHattrickId
        {
            get { return this.GetValue(TeamHattrickIdProperty); }
            set { this.SetValue(TeamHattrickIdProperty, value); }
        }

        public byte[]? TeamLogoBytes
        {
            get { return this.GetValue(TeamLogoBytesProperty); }
            set { this.SetValue(TeamLogoBytesProperty, value); }
        }

        public string TeamName
        {
            get { return this.GetValue(TeamNameProperty); }
            set { this.SetValue(TeamNameProperty, value); }
        }
    }
}