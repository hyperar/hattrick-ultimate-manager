namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record LastMatch(
        long MatchId,
        DateTime Date,
        long HomeTeamId,
        string HomeTeamName,
        int HomeTeamGoals,
        long AwayTeamId,
        string AwayTeamName,
        int AwayTeamGoals)
    {
        public bool Equals(LastMatch? other)
        {
            return other != null
                 && this.MatchId == other.MatchId
                 && this.Date == other.Date
                 && this.HomeTeamId == other.HomeTeamId
                 && this.HomeTeamName == other.HomeTeamName
                 && this.HomeTeamGoals == other.HomeTeamGoals
                 && this.AwayTeamId == other.AwayTeamId
                 && this.AwayTeamName == other.AwayTeamName
                 && this.AwayTeamGoals == other.AwayTeamGoals;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.MatchId);
            hash.Add(this.Date);
            hash.Add(this.HomeTeamId);
            hash.Add(this.HomeTeamName);
            hash.Add(this.HomeTeamGoals);
            hash.Add(this.AwayTeamId);
            hash.Add(this.AwayTeamName);
            hash.Add(this.AwayTeamGoals);

            return hash.ToHashCode();
        }
    }
}