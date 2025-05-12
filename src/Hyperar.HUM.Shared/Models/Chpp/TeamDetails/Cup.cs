namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    public record Cup(
        bool StillInCup,
        long? CupId,
        string? CupName,
        int? CupLeagueLevel,
        int? CupLevel,
        int? CupLevelIndex,
        int? MatchRound,
        int? MatchRoundsLeft);
}