namespace Hyperar.HUM.Shared.Models.Chpp.WorldDetails
{
    public record Cup(
        long CupId,
        string CupName,
        int CupLeagueLevel,
        int CupLevel,
        int CupLevelIndex,
        int MatchRound,
        int MatchRoundsLeft);
}