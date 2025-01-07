namespace Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium
{
    public record Team(
        long TeamId,
        string TeamName,
        IdName Arena,
        League League,
        IdName Country,
        IdName LeagueLevelUnit,
        IdName Region,
        YouthTeam? YouthTeam);
}