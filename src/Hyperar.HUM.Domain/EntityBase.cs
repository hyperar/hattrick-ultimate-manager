namespace Hyperar.HUM.Domain
{
    using System;
    using Hyperar.HUM.Domain.Interfaces;

    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
    }
}