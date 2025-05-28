namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;
    using System.Linq;

    public sealed record MySupporters(int TotalItems, int MaxItems, SupporterTeam[] Teams)
    {
        public bool Equals(MySupporters? other)
        {
            return other != null
                && this.TotalItems == other.TotalItems
                && this.MaxItems == other.MaxItems
                && this.Teams.SequenceEqual(other.Teams);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.TotalItems);
            hash.Add(this.MaxItems);
            hash.Add(this.Teams);

            return hash.ToHashCode();
        }
    }
}