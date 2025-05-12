namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public record LastMatch(
        long MatchId,
        DateTime Date,
        long HomeTeamId,
        string HomeTeamName,
        int HomeTeamGoals,
        long AwayTeamId,
        string AwayTeamName,
        int AwayTeamGoals);
}