namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    public record SupportedTeam(
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
        PressAnnouncement? PressAnnouncement);
}