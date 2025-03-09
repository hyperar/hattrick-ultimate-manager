namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System.Collections.Generic;

    public record MySupporters(
        int TotalItems,
        int MaxItems,
        IEnumerable<SupporterTeam> Teams);
}