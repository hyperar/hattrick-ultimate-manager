namespace Hyperar.HUM.Shared.Models.Chpp.Players
{
    using System;

    public sealed record HattrickData(
        string FileName,
        decimal Version,
        long UserId,
        DateTime FetchedDate,
        string UserSupporter,
        bool IsYouth,
        string ActionType,
        bool IsPlayingMatch,
        Team Team) : XmlFileBase(FileName, Version, UserId, FetchedDate)
    {
        public bool Equals(HattrickData? other)
        {
            return other != null
                && this.FileName == other.FileName
                && this.Version == other.Version
                && this.UserId == other.UserId
                && this.FetchedDate == other.FetchedDate
                && this.Team == other.Team;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.FileName);
            hash.Add(this.Version);
            hash.Add(this.UserId);
            hash.Add(this.FetchedDate);
            hash.Add(this.Team);

            return hash.ToHashCode();
        }
    }
}