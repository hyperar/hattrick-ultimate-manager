namespace Hyperar.HUM.Shared.Models.Chpp.WorldDetails
{
    using System;

    public record League(
        long LeagueId,
        string LeagueName,
        int Season,
        int SeasonOffset,
        int MatchRound,
        string ShortName,
        string Continent,
        string ZoneName,
        string EnglishName,
        long LanguageId,
        string LanguageName,
        Country Country,
        Cup[] Cups,
        long NationalTeamId,
        long U20TeamId,
        int ActiveTeams,
        int ActiveUsers,
        int WaitingUsers,
        DateTime TrainingDate,
        DateTime EconomyDate,
        DateTime CupMatchDate,
        DateTime SeriesMatchDate,
        DateTime Sequence1,
        DateTime Sequence2,
        DateTime Sequence3,
        DateTime Sequence5,
        DateTime Sequence7,
        int NumberOfLevels);
}