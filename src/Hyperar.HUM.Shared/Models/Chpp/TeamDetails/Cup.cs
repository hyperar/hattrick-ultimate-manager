namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record Cup(
        bool StillInCup,
        long? CupId,
        string? CupName,
        int? CupLeagueLevel,
        int? CupLevel,
        int? CupLevelIndex,
        int? MatchRound,
        int? MatchRoundsLeft)
    {
        public bool Equals(Cup? other)
        {
            return other != null
                && this.StillInCup == other.StillInCup
                && this.CupId == other.CupId
                && this.CupName == other.CupName
                && this.CupLeagueLevel == other.CupLeagueLevel
                && this.CupLevel == other.CupLevel
                && this.CupLevelIndex == other.CupLevelIndex
                && this.MatchRound == other.MatchRound
                && this.MatchRoundsLeft == other.MatchRoundsLeft;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.StillInCup);
            hash.Add(this.CupId);
            hash.Add(this.CupName);
            hash.Add(this.CupLeagueLevel);
            hash.Add(this.CupLevel);
            hash.Add(this.CupLevelIndex);
            hash.Add(this.MatchRound);
            hash.Add(this.MatchRoundsLeft);

            return hash.ToHashCode();
        }
    }
}