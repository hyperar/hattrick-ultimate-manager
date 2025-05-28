namespace Hyperar.HUM.Shared.Models.Chpp.Players
{
    using System;

    public sealed record TrainerData(int TrainerType, int TrainerSkillLevel)
    {
        public bool Equals(TrainerData? other)
        {
            return other != null
                && this.TrainerType == other.TrainerType
                && this.TrainerSkillLevel == other.TrainerSkillLevel;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.TrainerType);
            hash.Add(this.TrainerSkillLevel);

            return hash.ToHashCode();
        }
    }
}
