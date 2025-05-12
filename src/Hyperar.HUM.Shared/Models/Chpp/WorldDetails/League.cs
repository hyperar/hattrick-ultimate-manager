namespace Hyperar.HUM.Shared.Models.Chpp.WorldDetails
{
    using System;
    using System.Linq;

    public sealed record League(
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
        int NumberOfLevels)
    {
        public bool Equals(League? other)
        {
            return other != null
                && this.LeagueId == other.LeagueId
                && this.LeagueName == other.LeagueName
                && this.Season == other.Season
                && this.SeasonOffset == other.SeasonOffset
                && this.MatchRound == other.MatchRound
                && this.ShortName == other.ShortName
                && this.Continent == other.Continent
                && this.ZoneName == other.ZoneName
                && this.EnglishName == other.EnglishName
                && this.LanguageId == other.LanguageId
                && this.LanguageName == other.LanguageName
                && this.Country == other.Country
                && this.Cups.SequenceEqual(other.Cups)
                && this.NationalTeamId == other.NationalTeamId
                && this.U20TeamId == other.U20TeamId
                && this.ActiveTeams == other.ActiveTeams
                && this.ActiveUsers == other.ActiveUsers
                && this.WaitingUsers == other.WaitingUsers
                && this.TrainingDate == other.TrainingDate
                && this.EconomyDate == other.EconomyDate
                && this.CupMatchDate == other.CupMatchDate
                && this.SeriesMatchDate == other.SeriesMatchDate
                && this.Sequence1 == other.Sequence1
                && this.Sequence2 == other.Sequence2
                && this.Sequence3 == other.Sequence3
                && this.Sequence5 == other.Sequence5
                && this.Sequence7 == other.Sequence7
                && this.NumberOfLevels == other.NumberOfLevels;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.LeagueId);
            hash.Add(this.LeagueName);
            hash.Add(this.Season);
            hash.Add(this.SeasonOffset);
            hash.Add(this.MatchRound);
            hash.Add(this.ShortName);
            hash.Add(this.Continent);
            hash.Add(this.ZoneName);
            hash.Add(this.EnglishName);
            hash.Add(this.LanguageId);
            hash.Add(this.LanguageName);
            hash.Add(this.Country);
            hash.Add(this.Cups);
            hash.Add(this.NationalTeamId);
            hash.Add(this.U20TeamId);
            hash.Add(this.ActiveTeams);
            hash.Add(this.ActiveUsers);
            hash.Add(this.WaitingUsers);
            hash.Add(this.TrainingDate);
            hash.Add(this.EconomyDate);
            hash.Add(this.CupMatchDate);
            hash.Add(this.SeriesMatchDate);
            hash.Add(this.Sequence1);
            hash.Add(this.Sequence2);
            hash.Add(this.Sequence3);
            hash.Add(this.Sequence5);
            hash.Add(this.Sequence7);
            hash.Add(this.NumberOfLevels);

            return hash.ToHashCode();
        }
    }
}