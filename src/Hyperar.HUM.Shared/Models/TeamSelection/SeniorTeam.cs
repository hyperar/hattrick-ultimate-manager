namespace Hyperar.HUM.Shared.Models.TeamSelection
{
    public record SeniorTeam(
        IdName Team,
        IdName Country,
        IdName Region,
        IdName League,
        IdName Series,
        byte[]? LogoBytes,
        byte[] CountryFlagBytes,
        byte[] LeagueFlagBytes);
}