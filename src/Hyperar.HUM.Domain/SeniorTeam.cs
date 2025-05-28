namespace Hyperar.HUM.Domain
{
    using System;

    public class SeniorTeam : HattrickEntityBase
    {
        public byte[] AwayMatchKitBytes { get; set; } = Array.Empty<byte>();

        public bool CanBookMidweekFriendly { get; set; }

        public bool CanBookWeekendFriendly { get; set; }

        public DateTime FoundedOn { get; set; }

        public int GlobalPowerRating { get; set; }

        public byte[] HomeMatchKitBytes { get; set; } = Array.Empty<byte>();

        public bool IsPlayingCup { get; set; }

        public virtual League League { get; set; } = new League();

        public long LeagueHattrickId { get; set; }

        public int LeaguePowerRating { get; set; }

        public int LeagueRank { get; set; }

        public byte[]? LogoBytes { get; set; }

        public virtual Manager Manager { get; set; } = new Manager();

        public long ManagerHattrickId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int PowerRating { get; set; }

        public virtual Region Region { get; set; } = new Region();

        public long RegionHattrickId { get; set; }

        public int RegionPowerRating { get; set; }

        public long SeriesHattrickId { get; set; }

        public string SeriesName { get; set; } = string.Empty;

        public string ShortName { get; set; } = string.Empty;

        public int TeamIndex { get; set; }

        public int UndefeatedStreak { get; set; }

        public int WinningStreak { get; set; }
    }
}