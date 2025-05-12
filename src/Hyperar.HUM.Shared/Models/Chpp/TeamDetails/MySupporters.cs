namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    public record MySupporters(
        int TotalItems,
        int MaxItems,
        SupporterTeam[] Teams);
}