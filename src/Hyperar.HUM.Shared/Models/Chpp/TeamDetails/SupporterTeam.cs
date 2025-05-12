namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    public record SupporterTeam(
        long UserId,
        string LoginName,
        long TeamId,
        string TeamName,
        long LeagueId,
        string LeagueName,
        long LeagueLevelUnitId,
        string LeagueLevelUnitName);
}