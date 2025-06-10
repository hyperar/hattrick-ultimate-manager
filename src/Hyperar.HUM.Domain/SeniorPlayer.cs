namespace Hyperar.HUM.Domain
{
    using System;
    using System.Collections.Generic;
    using Hyperar.HUM.Shared.Enums;

    public class SeniorPlayer : HattrickEntityBase
    {
        public int AgeDays { get; set; }

        public int AgeYears { get; set; }

        public AggressivenessLevel Aggressiveness { get; set; }

        public AgreeabilityLevel Agreeability { get; set; }

        public virtual ICollection<SeniorPlayerAvatarLayer> AvatarLayers { get; set; } = new HashSet<SeniorPlayerAvatarLayer>();

        public int BookingStatus { get; set; }

        public int CareerAssists { get; set; }

        public int CareerGoals { get; set; }

        public int CareerHattricks { get; set; }

        public int? Category { get; set; }

        public virtual Country Country { get; set; } = new Country();

        public long CountryHattrickId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public bool HasMotherClubBonus { get; set; }

        public int HealthStatus { get; set; }

        public HonestyLevel Honesty { get; set; }

        public DateTime JoinedOn { get; set; }

        public int JuniorNationalTeamMatches { get; set; }

        public string LastName { get; set; } = string.Empty;

        public LeadershipSkillLevel Leadership { get; set; }

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

        public PlayerSpecialty Specialty { get; set; }

        public string? Statement { get; set; }

        public int TeamAssists { get; set; }

        public int TeamGoals { get; set; }

        public int TeamMatches { get; set; }
    }
}