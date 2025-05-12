namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public record NextMatch(
        long MatchId,
        DateTime Date,
        long HomeTeamId,
        string HomeTeamName,
        long AwayTeamId,
        string AwayTeamName);
}