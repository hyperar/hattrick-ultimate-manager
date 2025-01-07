namespace Hyperar.HUM.Domain
{
    public class Region : HattrickEntityBase
    {
        public virtual Country Country { get; set; } = new Country();

        public long CountryHattrickId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}