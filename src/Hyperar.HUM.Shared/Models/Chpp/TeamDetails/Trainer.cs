namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record Trainer(long PlayerId)
    {
        public bool Equals(Trainer? other)
        {
            return other != null
                && this.PlayerId == other.PlayerId;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.PlayerId);

            return hash.ToHashCode();
        }
    }
}