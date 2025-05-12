namespace Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium
{
    using System;

    public sealed record League(long LeagueId, string LeagueName, int Season)
    {
        public bool Equals(League? other)
        {
            return other != null
                && this.LeagueId == other.LeagueId
                && this.LeagueName == other.LeagueName
                && this.Season == other.Season;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.LeagueId);
            hash.Add(this.LeagueName);
            hash.Add(this.Season);

            return hash.ToHashCode();
        }
    }
}