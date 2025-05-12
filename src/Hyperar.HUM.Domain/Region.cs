namespace Hyperar.HUM.Domain
{
    using System.Collections.Generic;

    public class Region : HattrickEntityBase
    {
        public virtual Country Country { get; set; } = new Country();

        public long CountryHattrickId { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<SeniorTeam> SeniorTeams { get; set; } = new HashSet<SeniorTeam>();
    }
}