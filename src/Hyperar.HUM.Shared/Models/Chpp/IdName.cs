namespace Hyperar.HUM.Shared.Models.Chpp
{
    using System;

    public sealed record IdName(long Id, string Name)
    {
        public bool Equals(IdName? other)
        {
            return other != null
                && this.Id == other.Id
                && this.Name == other.Name;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.Id);
            hash.Add(this.Name);

            return hash.ToHashCode();
        }
    }
}