namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;
    using System.Linq;

    public sealed record Flags(Flag[] AwayFlags, Flag[] HomeFlags)
    {
        public bool Equals(Flags? other)
        {
            return other != null
                && this.AwayFlags.SequenceEqual(other.AwayFlags)
                && this.HomeFlags.SequenceEqual(other.HomeFlags);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.AwayFlags);
            hash.Add(this.HomeFlags);

            return hash.ToHashCode();
        }
    }
}