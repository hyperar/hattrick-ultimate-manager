namespace Hyperar.HUM.Domain
{
    using Hyperar.HUM.Domain.Interfaces;

    public abstract class HattrickEntityBase : IHattrickEntity
    {
        public long HattrickId { get; set; }
    }
}