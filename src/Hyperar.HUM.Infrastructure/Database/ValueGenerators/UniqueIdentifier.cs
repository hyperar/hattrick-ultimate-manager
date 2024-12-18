namespace Hyperar.HUM.Infrastructure.Database.ValueGenerators
{
    using System;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.ValueGeneration;

    internal class UniqueIdentifier : ValueGenerator<Guid>
    {
        public override bool GeneratesTemporaryValues
        {
            get
            {
                return false;
            }
        }

        public override Guid Next(EntityEntry entry)
        {
            return Guid.NewGuid();
        }
    }
}