namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;
    using System.Linq;

    public sealed record Team(
        long TeamId,
        string TeamName,
        string ShortTeamName,
        bool IsPrimaryClub,
        DateTime FoundedDate,
        bool IsDeactivated,
        IdName Arena,
        IdName League,
        IdName Country,
        IdName Region,
        Trainer Trainer,
        string HomePage,
        string DressUri,
        string AlternateDressUri,
        LeagueLevelUnit LeagueLevelUnit,
        BotStatus BotStatus,
        Cup? Cup,
        PowerRating PowerRating,
        long? FriendlyTeamId,
        int? NumberOfVictories,
        int? NumberOfUndefeated,
        int? TeamRank,
        Fanclub Fanclub,
        string LogoUrl,
        Guestbook? Guestbook,
        PressAnnouncement? PressAnnouncement,
        TeamColors? TeamColors,
        long YouthTeamId,
        string YouthTeamName,
        int NumberOfVisits,
        Flags? Flags,
        Trophy[] TrophyList,
        SupportedTeams? SupportedTeams,
        MySupporters? MySupporters,
        bool PossibleToChallengeMidweek,
        bool PossibleToChallengeWeekend)
    {
        public bool Equals(Team? other)
        {
            return other != null
                && this.TeamId == other.TeamId
                && this.TeamName == other.TeamName
                && this.ShortTeamName == other.ShortTeamName
                && this.IsPrimaryClub == other.IsPrimaryClub
                && this.FoundedDate == other.FoundedDate
                && this.IsDeactivated == other.IsDeactivated
                && this.Arena == other.Arena
                && this.League == other.League
                && this.Country == other.Country
                && this.Region == other.Region
                && this.Trainer == other.Trainer
                && this.HomePage == other.HomePage
                && this.DressUri == other.DressUri
                && this.AlternateDressUri == other.AlternateDressUri
                && this.LeagueLevelUnit == other.LeagueLevelUnit
                && this.BotStatus == other.BotStatus
                && this.Cup == other.Cup
                && this.PowerRating == other.PowerRating
                && this.FriendlyTeamId == other.FriendlyTeamId
                && this.NumberOfVictories == other.NumberOfVictories
                && this.NumberOfUndefeated == other.NumberOfUndefeated
                && this.TeamRank == other.TeamRank
                && this.Fanclub == other.Fanclub
                && this.LogoUrl == other.LogoUrl
                && this.Guestbook == other.Guestbook
                && this.PressAnnouncement == other.PressAnnouncement
                && this.TeamColors == other.TeamColors
                && this.YouthTeamId == other.YouthTeamId
                && this.YouthTeamName == other.YouthTeamName
                && this.NumberOfVisits == other.NumberOfVisits
                && this.Flags == other.Flags
                && this.TrophyList.SequenceEqual(other.TrophyList)
                && this.SupportedTeams == other.SupportedTeams
                && this.MySupporters == other.MySupporters
                && this.PossibleToChallengeMidweek == other.PossibleToChallengeMidweek
                && this.PossibleToChallengeWeekend == other.PossibleToChallengeWeekend;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.TeamId);
            hash.Add(this.TeamName);
            hash.Add(this.ShortTeamName);
            hash.Add(this.IsPrimaryClub);
            hash.Add(this.FoundedDate);
            hash.Add(this.IsDeactivated);
            hash.Add(this.Arena);
            hash.Add(this.League);
            hash.Add(this.Country);
            hash.Add(this.Region);
            hash.Add(this.Trainer);
            hash.Add(this.HomePage);
            hash.Add(this.DressUri);
            hash.Add(this.AlternateDressUri);
            hash.Add(this.LeagueLevelUnit);
            hash.Add(this.BotStatus);
            hash.Add(this.Cup);
            hash.Add(this.PowerRating);
            hash.Add(this.FriendlyTeamId);
            hash.Add(this.NumberOfVictories);
            hash.Add(this.NumberOfUndefeated);
            hash.Add(this.TeamRank);
            hash.Add(this.Fanclub);
            hash.Add(this.LogoUrl);
            hash.Add(this.Guestbook);
            hash.Add(this.PressAnnouncement);
            hash.Add(this.TeamColors);
            hash.Add(this.YouthTeamId);
            hash.Add(this.YouthTeamName);
            hash.Add(this.NumberOfVisits);
            hash.Add(this.Flags);
            hash.Add(this.TrophyList);
            hash.Add(this.SupportedTeams);
            hash.Add(this.MySupporters);
            hash.Add(this.PossibleToChallengeMidweek);
            hash.Add(this.PossibleToChallengeWeekend);

            return hash.ToHashCode();
        }
    }
}