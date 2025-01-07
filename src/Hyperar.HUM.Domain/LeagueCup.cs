namespace Hyperar.HUM.Domain
{
    using Hyperar.HUM.Shared.Enums;

    public class LeagueCup : HattrickEntityBase
    {
        public virtual League League { get; set; } = new League();

        public long LeagueHattrickId { get; set; }

        public int? Level { get; set; }

        public string Name { get; set; } = string.Empty;

        public LeagueCupSubType? SubType { get; set; }

        public LeagueCupType Type { get; set; }

        public int Week { get; set; }

        public int WeeksLeft { get; set; }
    }
}