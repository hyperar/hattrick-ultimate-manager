namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record SupportedTeam(
        long UserId,
        string LoginName,
        long TeamId,
        string TeamName,
        long LeagueId,
        string LeagueName,
        long LeagueLevelUnitId,
        string LeagueLevelUnitName,
        LastMatch? LastMatch,
        NextMatch? NextMatch,
        PressAnnouncement? PressAnnouncement)
    {
        public bool Equals(SupportedTeam? other)
        {
            return other != null
                && this.UserId == other.UserId
                && this.LoginName == other.LoginName
                && this.TeamId == other.TeamId
                && this.TeamName == other.TeamName
                && this.LeagueId == other.LeagueId
                && this.LeagueName == other.LeagueName
                && this.LeagueLevelUnitId == other.LeagueLevelUnitId
                && this.LeagueLevelUnitName == other.LeagueLevelUnitName
                && this.LastMatch == other.LastMatch
                && this.NextMatch == other.NextMatch
                && this.PressAnnouncement == other.PressAnnouncement;
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
            hash.Add(this.LastMatch);
            hash.Add(this.NextMatch);
            hash.Add(this.PressAnnouncement);

            return hash.ToHashCode();
        }
    }
}