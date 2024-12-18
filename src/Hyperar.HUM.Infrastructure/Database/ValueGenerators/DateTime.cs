namespace Hyperar.HUM.Infrastructure.Database.ValueGenerators
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.ValueGeneration;

    internal class DateTime : ValueGenerator<System.DateTime>
    {
        public override bool GeneratesTemporaryValues
        {
            get
            {
                return false;
            }
        }

        public override System.DateTime Next(EntityEntry entry)
        {
            return System.DateTime.Now;
        }
    }
}