namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public record Trophy(
        long TrophyId,
        int TrophySeason,
        int LeagueLevel,
        long LeagueLevelUnitId,
        string LeagueLevelUnitName,
        DateTime GainedDate,
        string ImageUrl,
        int? CupLeagueLevel,
        int? CupLevel,
        int? CupLevelIndex);
}