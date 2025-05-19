namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record Flag(long LeagueId, string LeagueName, string CountryCode)
    {
        public bool Equals(Flag? other)
        {
            return other != null
                && this.LeagueId == other.LeagueId
                && this.LeagueName == other.LeagueName
                && this.CountryCode == other.CountryCode;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.LeagueId);
            hash.Add(this.LeagueName);
            hash.Add(this.CountryCode);

            return hash.ToHashCode();
        }
    }
}