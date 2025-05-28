namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record SupporterTeam(
        long UserId,
        string LoginName,
        long TeamId,
        string TeamName,
        long LeagueId,
        string LeagueName,
        long LeagueLevelUnitId,
        string LeagueLevelUnitName)
    {
        public bool Equals(SupporterTeam? other)
        {
            return other != null
                && this.UserId == other.UserId
                && this.LoginName == other.LoginName
                && this.TeamId == other.TeamId
                && this.TeamName == other.TeamName
                && this.LeagueId == other.LeagueId
                && this.LeagueName == other.LeagueName
                && this.LeagueLevelUnitId == other.LeagueLevelUnitId
                && this.LeagueLevelUnitName == other.LeagueLevelUnitName;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.UserId);
            hash.Add(this.LoginName);
            hash.Add(this.TeamId);
            hash.Add(this.TeamName);
            hash.Add(this.LeagueId);
            hash.Add(this.LeagueName);
            hash.Add(this.LeagueLevelUnitId);
            hash.Add(this.LeagueLevelUnitName);

            return hash.ToHashCode();
        }
    }
}