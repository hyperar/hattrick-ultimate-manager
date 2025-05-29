namespace Hyperar.HUM.Shared.Models.Chpp.Players
{
    using System;

    public sealed record Player(
        long PlayerId,
        string FirstName,
        string NickName,
        string LastName,
        int PlayerNumber,
        int Age,
        int AgeDays,
        DateTime ArrivalDate,
        string OwnerNotes,
        int TSI,
        int PlayerForm,
        string Statement,
        int Experience,
        int Loyalty,
        bool MotherClubBonus,
        int Leadership,
        int Salary,
        bool IsAbroad,
        int Agreeability,
        int Aggressiveness,
        int Honesty,
        int LeagueGoals,
        int CupGoals,
        int FriendliesGoals,
        int CareerGoals,
        int CareerHattricks,
        int MatchesCurrentTeam,
        int GoalsCurrentTeam,
        int AssistsCurrentTeam,
        int CareerAssists,
        int Specialty,
        bool TransferListed,
        long NationalTeamId,
        long CountryId,
        int Caps,
        int CapsU20,
        int Cards,
        int InjuryLevel,
        int? StaminaSkill,
        int? KeeperSkill,
        int? PlaymakerSkill,
        int? ScorerSkill,
        int? PassingSkill,
        int? WingerSkill,
        int? DefenderSkill,
        int? SetPiecesSkill,
        int? PlayerCategoryId,
        OwningTeam? OwningTeam,
        TrainerData? TrainerData,
        LastMatch? LastMatch)
    {
        public bool Equals(Player? other)
        {
            return other != null
                && this.PlayerId == other.PlayerId
                && this.FirstName == other.FirstName
                && this.NickName == other.NickName
                && this.LastName == other.LastName
                && this.PlayerNumber == other.PlayerNumber
                && this.Age == other.Age
                && this.AgeDays == other.AgeDays
                && this.ArrivalDate == other.ArrivalDate
                && this.OwnerNotes == other.OwnerNotes
                && this.TSI == other.TSI
                && this.PlayerForm == other.PlayerForm
                && this.Statement == other.Statement
                && this.Experience == other.Experience
                && this.Loyalty == other.Loyalty
                && this.MotherClubBonus == other.MotherClubBonus
                && this.Leadership == other.Leadership
                && this.Salary == other.Salary
                && this.IsAbroad == other.IsAbroad
                && this.Agreeability == other.Agreeability
                && this.Aggressiveness == other.Aggressiveness
                && this.Honesty == other.Honesty
                && this.LeagueGoals == other.LeagueGoals
                && this.CupGoals == other.CupGoals
                && this.FriendliesGoals == other.FriendliesGoals
                && this.CareerGoals == other.CareerGoals
                && this.CareerHattricks == other.CareerHattricks
                && this.MatchesCurrentTeam == other.MatchesCurrentTeam
                && this.GoalsCurrentTeam == other.GoalsCurrentTeam
                && this.AssistsCurrentTeam == other.AssistsCurrentTeam
                && this.CareerAssists == other.CareerAssists
                && this.Specialty == other.Specialty
                && this.TransferListed == other.TransferListed
                && this.NationalTeamId == other.NationalTeamId
                && this.CountryId == other.CountryId
                && this.Caps == other.Caps
                && this.CapsU20 == other.CapsU20
                && this.Cards == other.Cards
                && this.InjuryLevel == other.InjuryLevel
                && this.StaminaSkill == other.StaminaSkill
                && this.KeeperSkill == other.KeeperSkill
                && this.PlaymakerSkill == other.PlaymakerSkill
                && this.ScorerSkill == other.ScorerSkill
                && this.PassingSkill == other.PassingSkill
                && this.WingerSkill == other.WingerSkill
                && this.DefenderSkill == other.DefenderSkill
                && this.SetPiecesSkill == other.SetPiecesSkill
                && this.PlayerCategoryId == other.PlayerCategoryId
                && this.TrainerData == other.TrainerData
                && this.OwningTeam == other.OwningTeam
                && this.LastMatch == other.LastMatch;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.PlayerId);
            hash.Add(this.FirstName);
            hash.Add(this.NickName);
            hash.Add(this.LastName);
            hash.Add(this.PlayerNumber);
            hash.Add(this.Age);
            hash.Add(this.AgeDays);
            hash.Add(this.ArrivalDate);
            hash.Add(this.OwnerNotes);
            hash.Add(this.TSI);
            hash.Add(this.PlayerForm);
            hash.Add(this.Statement);
            hash.Add(this.Experience);
            hash.Add(this.Loyalty);
            hash.Add(this.MotherClubBonus);
            hash.Add(this.Leadership);
            hash.Add(this.Salary);
            hash.Add(this.IsAbroad);
            hash.Add(this.Agreeability);
            hash.Add(this.Aggressiveness);
            hash.Add(this.Honesty);
            hash.Add(this.LeagueGoals);
            hash.Add(this.CupGoals);
            hash.Add(this.FriendliesGoals);
            hash.Add(this.CareerGoals);
            hash.Add(this.CareerHattricks);
            hash.Add(this.MatchesCurrentTeam);
            hash.Add(this.GoalsCurrentTeam);
            hash.Add(this.AssistsCurrentTeam);
            hash.Add(this.CareerAssists);
            hash.Add(this.Specialty);
            hash.Add(this.TransferListed);
            hash.Add(this.NationalTeamId);
            hash.Add(this.CountryId);
            hash.Add(this.Caps);
            hash.Add(this.CapsU20);
            hash.Add(this.Cards);
            hash.Add(this.InjuryLevel);
            hash.Add(this.StaminaSkill);
            hash.Add(this.KeeperSkill);
            hash.Add(this.PlaymakerSkill);
            hash.Add(this.ScorerSkill);
            hash.Add(this.PassingSkill);
            hash.Add(this.WingerSkill);
            hash.Add(this.DefenderSkill);
            hash.Add(this.SetPiecesSkill);
            hash.Add(this.PlayerCategoryId);
            hash.Add(this.TrainerData);
            hash.Add(this.OwningTeam);
            hash.Add(this.LastMatch);

            return hash.ToHashCode();
        }
    }
}