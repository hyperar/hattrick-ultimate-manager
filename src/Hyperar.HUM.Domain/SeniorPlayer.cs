namespace Hyperar.HUM.Domain
{
    using System;
    using System.Collections.Generic;

    public class SeniorPlayer : HattrickEntityBase
    {
        public int AgeDays { get; set; }

        public int AgeYears { get; set; }

        public int Aggressiveness { get; set; }

        public int Agreeability { get; set; }

        public int BookingStatus { get; set; }

        public int CareerAssists { get; set; }

        public int CareerGoals { get; set; }

        public int CareerHattricks { get; set; }

        public int? Category { get; set; }

        public virtual Country Country { get; set; } = new Country();

        public long CountryHattrickId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public int Form { get; set; }

        public bool HasMotherClubBonus { get; set; }

        public int HealthStatus { get; set; }

        public int Honesty { get; set; }

        public DateTime JoinedOn { get; set; }

        public int JuniorNationalTeamMatches { get; set; }

        public string LastName { get; set; } = string.Empty;

        public int Leadership { get; set; }

        public int Loyalty { get; set; }

        public string? NickName { get; set; }

        public string? Notes { get; set; }

        public int Salary { get; set; }

        public int SeasonCupGoals { get; set; }

        public int SeasonFriendlyGoals { get; set; }

        public int SeasonSeriesGoals { get; set; }

        public int SeniorNationalTeamMatches { get; set; }

        public virtual ICollection<SeniorPlayerSkillSet> SeniorPlayerSkillSets { get; set; } = new HashSet<SeniorPlayerSkillSet>();

        public virtual SeniorTeam SeniorTeam { get; set; } = new SeniorTeam();

        public long SeniorTeamHattrickId { get; set; }

        public int? ShirtNumber { get; set; }

        public int Specialty { get; set; }

        public int Stamina { get; set; }

        public string? Statement { get; set; }

        public int TeamAssists { get; set; }

        public int TeamGoals { get; set; }

        public int TeamMatches { get; set; }
    }
}