namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record PowerRating(int GlobalRanking, int LeagueRanking, int RegionRanking, int Value)
    {
        public bool Equals(PowerRating? other)
        {
            return other != null
                && this.GlobalRanking == other.GlobalRanking
                && this.LeagueRanking == other.LeagueRanking
                && this.RegionRanking == other.RegionRanking
                && this.Value == other.Value;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.GlobalRanking);
            hash.Add(this.LeagueRanking);
            hash.Add(this.RegionRanking);
            hash.Add(this.Value);

            return hash.ToHashCode();
        }
    }
}