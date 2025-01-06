namespace Hyperar.HUM.Domain
{
    using System;
    using System.Collections.Generic;

    public class Country : HattrickEntityBase
    {
        public string Code { get; set; } = string.Empty;

        public virtual Currency Currency { get; set; } = new Currency();

        public Guid CurrencyId { get; set; }

        public string DateFormat { get; set; } = string.Empty;

        public virtual League League { get; set; } = new League();

        public long LeagueHattrickId { get; set; }

        public virtual ICollection<Manager> Managers { get; set; } = new HashSet<Manager>();

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Region> Regions { get; set; } = new HashSet<Region>();

        public string TimeFormat { get; set; } = string.Empty;
    }
}