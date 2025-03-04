namespace Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium
{
    public record Manager(
        long UserId,
        string LoginName,
        string SupporterTier,
        string[] LastLogins,
        IdName Language,
        IdName Country,
        Currency Currency,
        Team[] Teams,
        IdName[]? NationalTeamCoach,
        IdName[]? NationalTeamAssistant,
        Avatar? Avatar);
}