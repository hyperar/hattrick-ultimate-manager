namespace Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium
{
    using System;

    public sealed record YouthTeam(long YouthTeamId, string YouthTeamName, IdName? YouthLeague)
    {
        public bool Equals(YouthTeam? other)
        {
            return other != null
                && this.YouthTeamId == other.YouthTeamId
                && this.YouthTeamName == other.YouthTeamName
                && this.YouthLeague == other.YouthLeague;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.YouthTeamId);
            hash.Add(this.YouthTeamName);
            hash.Add(this.YouthLeague);

            return hash.ToHashCode();
        }
    }
}