namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record Trophy(
        long TrophyId,
        int TrophySeason,
        int LeagueLevel,
        long LeagueLevelUnitId,
        string LeagueLevelUnitName,
        DateTime GainedDate,
        string? ImageUrl,
        int? CupLeagueLevel,
        int? CupLevel,
        int? CupLevelIndex)
    {
        public bool Equals(Trophy? other)
        {
            return other != null
                && this.TrophyId == other.TrophyId
                && this.TrophySeason == other.TrophySeason
                && this.LeagueLevel == other.LeagueLevel
                && this.LeagueLevelUnitId == other.LeagueLevelUnitId
                && this.LeagueLevelUnitName == other.LeagueLevelUnitName
                && this.GainedDate == other.GainedDate
                && this.ImageUrl == other.ImageUrl
                && this.CupLeagueLevel == other.CupLeagueLevel
                && this.CupLevel == other.CupLevel
                && this.CupLevelIndex == other.CupLevelIndex;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.TrophyId);
            hash.Add(this.TrophySeason);
            hash.Add(this.LeagueLevel);
            hash.Add(this.LeagueLevelUnitId);
            hash.Add(this.LeagueLevelUnitName);
            hash.Add(this.GainedDate);
            hash.Add(this.ImageUrl);
            hash.Add(this.CupLeagueLevel);
            hash.Add(this.CupLevel);
            hash.Add(this.CupLevelIndex);

            return hash.ToHashCode();
        }
    }
}