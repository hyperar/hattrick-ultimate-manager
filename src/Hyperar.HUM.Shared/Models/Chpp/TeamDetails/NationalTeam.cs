namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record NationalTeam(int NationalTeamStaffType, long NationalTeamId, string NationalTeamName)
    {
        public bool Equals(NationalTeam? other)
        {
            return other != null
                && this.NationalTeamStaffType == other.NationalTeamStaffType
                && this.NationalTeamId == other.NationalTeamId
                && this.NationalTeamName == other.NationalTeamName;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.NationalTeamStaffType);
            hash.Add(this.NationalTeamId);
            hash.Add(this.NationalTeamName);

            return hash.ToHashCode();
        }
    }
}