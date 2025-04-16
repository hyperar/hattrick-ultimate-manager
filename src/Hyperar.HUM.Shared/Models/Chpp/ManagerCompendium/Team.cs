namespace Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium
{
    using System;

    public sealed record Team(
        long TeamId,
        string TeamName,
        IdName Arena,
        League League,
        IdName Country,
        IdName LeagueLevelUnit,
        IdName Region,
        YouthTeam? YouthTeam)
    {
        public bool Equals(Team? other)
        {
            return other != null
                && this.TeamId == other.TeamId
                && this.TeamName == other.TeamName
                && this.Arena == other.Arena
                && this.League == other.League
                && this.Country == other.Country
                && this.LeagueLevelUnit == other.LeagueLevelUnit
                && this.Region == other.Region
                && this.YouthTeam == other.YouthTeam;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.TeamId);
            hash.Add(this.TeamName);
            hash.Add(this.Arena);
            hash.Add(this.League);
            hash.Add(this.Country);
            hash.Add(this.LeagueLevelUnit);
            hash.Add(this.Region);
            hash.Add(this.YouthTeam);

            return hash.ToHashCode();
        }
    }
}