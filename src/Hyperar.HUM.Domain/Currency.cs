namespace Hyperar.HUM.Domain
{
    using System.Collections.Generic;

    public class Currency : EntityBase
    {
        public decimal ConvertionRate { get; set; }

        public virtual ICollection<Country> Countries { get; set; } = new HashSet<Country>();

        public string Name { get; set; } = string.Empty;
    }
}