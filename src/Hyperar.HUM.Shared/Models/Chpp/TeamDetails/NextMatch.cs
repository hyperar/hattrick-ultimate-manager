namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record NextMatch(
        long MatchId,
        DateTime Date,
        long HomeTeamId,
        string HomeTeamName,
        long AwayTeamId,
        string AwayTeamName)
    {
        public bool Equals(NextMatch? other)
        {
            return other != null
                 && this.MatchId == other.MatchId
                 && this.Date == other.Date
                 && this.HomeTeamId == other.HomeTeamId
                 && this.HomeTeamName == other.HomeTeamName
                 && this.AwayTeamId == other.AwayTeamId
                 && this.AwayTeamName == other.AwayTeamName;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.MatchId);
            hash.Add(this.Date);
            hash.Add(this.HomeTeamId);
            hash.Add(this.HomeTeamName);
            hash.Add(this.AwayTeamId);
            hash.Add(this.AwayTeamName);

            return hash.ToHashCode();
        }
    }
}