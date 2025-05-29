namespace Hyperar.HUM.Shared.Models.Chpp.Players
{
    using System;

    public sealed record OwningTeam(long TeamId, string TeamName, string LeagueName)
    {
        public bool Equals(OwningTeam? other)
        {
            return other != null
                && this.TeamId == other.TeamId
                && this.TeamName == other.TeamName
                && this.LeagueName == other.LeagueName;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.TeamId);
            hash.Add(this.TeamName);
            hash.Add(this.LeagueName);

            return hash.ToHashCode();
        }
    }
}