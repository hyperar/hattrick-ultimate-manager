namespace Hyperar.HUM.Domain
{
    using System;
    using System.Collections.Generic;

    public class League : HattrickEntityBase
    {
        public int ActiveTeams { get; set; }

        public int ActiveUsers { get; set; }

        public string Continent { get; set; } = string.Empty;

        public virtual Country? Country { get; set; }

        public virtual ICollection<LeagueCup> Cups { get; set; } = new HashSet<LeagueCup>();

        public string EnglishName { get; set; } = string.Empty;

        public DateTime FifthDailyUpdate { get; set; }

        public DateTime FirstDailyUpdate { get; set; }

        public byte[] FlagBytes { get; set; } = Array.Empty<byte>();

        public DateTime FourthDailyUpdate { get; set; }

        public long? JuniorNationalTeamHattrickId { get; set; }

        public int LeagueLevels { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime? NextCupMatchDate { get; set; }

        public DateTime NextEconomyUpdate { get; set; }

        public DateTime? NextSeriesMatchDate { get; set; }

        public DateTime NextTrainingUpdate { get; set; }

        public int Season { get; set; }

        public int SeasonOffset { get; set; }

        public DateTime SecondDailyUpdate { get; set; }

        public long? SeniorNationalTeamHattrickId { get; set; }

        public string ShortName { get; set; } = string.Empty;

        public DateTime ThirdDailyUpdate { get; set; }

        public int WaitingUsers { get; set; }

        public int Week { get; set; }

        public string Zone { get; set; } = string.Empty;
    }
}