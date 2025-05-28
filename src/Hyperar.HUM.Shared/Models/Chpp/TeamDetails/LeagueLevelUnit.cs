namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record LeagueLevelUnit(long LeagueLevelUnitId, string LeagueLevelUnitName, int LeagueLevel)
    {
        public bool Equals(LeagueLevelUnit? other)
        {
            return other != null
                && this.LeagueLevelUnitId == other.LeagueLevelUnitId
                && this.LeagueLevelUnitName == other.LeagueLevelUnitName
                && this.LeagueLevel == other.LeagueLevel;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.LeagueLevelUnitId);
            hash.Add(this.LeagueLevelUnitName);
            hash.Add(this.LeagueLevel);

            return hash.ToHashCode();
        }
    }
}