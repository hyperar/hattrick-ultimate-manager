namespace Hyperar.HUM.Domain.Interfaces
{
    using System;

    public interface IEntity
    {
        public DateTime CreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public Guid Id { get; set; }

        public bool IsDeleted
        {
            get
            {
                return this.DeletedOn.HasValue && this.DeletedOn.Value <= DateTime.Now;
            }
        }

        public DateTime? UpdatedOn { get; set; }
    }
}