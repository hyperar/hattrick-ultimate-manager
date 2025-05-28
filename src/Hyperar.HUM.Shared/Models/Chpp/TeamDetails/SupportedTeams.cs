namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;
    using System.Linq;

    public sealed record SupportedTeams(int TotalItems, int MaxItems, SupportedTeam[] Teams)
    {
        public bool Equals(SupportedTeams? other)
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