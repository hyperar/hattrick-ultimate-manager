namespace Hyperar.HUM.Domain
{
    using Hyperar.HUM.Shared.Enums;

    public class SeniorPlayerSkillSet : EntityBase
    {
        public PlayerSkillLevel Defender { get; set; }

        public PlayerSkillLevel Experience { get; set; }

        public PlayerSkillLevel Form { get; set; }

        public PlayerSkillLevel Keeper { get; set; }

        public PlayerSkillLevel Loyalty { get; set; }

        public PlayerSkillLevel Passing { get; set; }

        public PlayerSkillLevel Playmaking { get; set; }

        public PlayerSkillLevel Scoring { get; set; }

        public int Season { get; set; }

        public virtual SeniorPlayer SeniorPlayer { get; set; } = new SeniorPlayer();

        public long SeniorPlayerHattrickId { get; set; }

        public PlayerSkillLevel SetPieces { get; set; }

        public PlayerSkillLevel Stamina { get; set; }

        public int TotalSkillIndex { get; set; }

        public int Week { get; set; }

        public PlayerSkillLevel Winger { get; set; }
    }
}