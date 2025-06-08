namespace Hyperar.HUM.Domain
{
    public class SeniorPlayerSkillSet : EntityBase
    {
        public int Defender { get; set; }

        public int Experience { get; set; }

        public int Keeper { get; set; }

        public int Passing { get; set; }

        public int Playmaking { get; set; }

        public int Scoring { get; set; }

        public int Season { get; set; }

        public virtual SeniorPlayer SeniorPlayer { get; set; } = new SeniorPlayer();

        public long SeniorPlayerHattrickId { get; set; }

        public int SetPieces { get; set; }

        public int TotalSkillIndex { get; set; }

        public int Week { get; set; }

        public int Winger { get; set; }
    }
}