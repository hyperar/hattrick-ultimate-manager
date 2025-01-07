namespace Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium
{
    using System.Collections.Generic;

    public record Manager(
        long UserId,
        string LoginName,
        string SupporterTier,
        IEnumerable<string> LastLogins,
        IdName Language,
        IdName Country,
        Currency Currency,
        IEnumerable<Team> Teams,
        IEnumerable<IdName>? NationalTeamCoach,
        IEnumerable<IdName>? NationalTeamAssistant,
        Avatar? Avatar);
}