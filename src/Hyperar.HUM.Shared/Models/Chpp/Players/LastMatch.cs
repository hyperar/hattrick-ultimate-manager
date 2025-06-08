namespace Hyperar.HUM.Shared.Models.Chpp.Players
{
    using System;

    public sealed record LastMatch(
        DateTime Date,
        long MatchId,
        int PositionCode,
        int PlayedMinutes,
        decimal Rating,
        decimal RatingEndOfMatch)
    {
        public bool Equals(LastMatch? other)
        {
            return other != null
                && this.Date == other.Date
                && this.MatchId == other.MatchId
                && this.PositionCode == other.PositionCode
                && this.PlayedMinutes == other.PlayedMinutes
                && this.Rating == other.Rating
                && this.RatingEndOfMatch == other.RatingEndOfMatch;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.Date);
            hash.Add(this.MatchId);
            hash.Add(this.PositionCode);
            hash.Add(this.PlayedMinutes);
            hash.Add(this.Rating);
            hash.Add(this.RatingEndOfMatch);

            return hash.ToHashCode();
        }
    }
}