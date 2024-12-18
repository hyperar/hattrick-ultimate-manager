namespace Hyperar.HUM.Domain
{
    using System;
    using Hyperar.HUM.Domain.Interfaces;

    public abstract class EntityBase : IEntity
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public Guid Id { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}