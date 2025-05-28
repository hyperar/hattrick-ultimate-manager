namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;
    using System.Linq;

    public sealed record HattrickData(
        string FileName,
        decimal Version,
        long UserId,
        DateTime FetchedDate,
        User User,
        Team[] Teams) : XmlFileBase(FileName, Version, UserId, FetchedDate)
    {
        public bool Equals(HattrickData? other)
        {
            return other != null
                && this.FileName == other.FileName
                && this.Version == other.Version
                && this.UserId == other.UserId
                && this.FetchedDate == other.FetchedDate
                && this.User == other.User
                && this.Teams.SequenceEqual(other.Teams);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.FileName);
            hash.Add(this.Version);
            hash.Add(this.UserId);
            hash.Add(this.FetchedDate);
            hash.Add(this.User);
            hash.Add(this.Teams);

            return hash.ToHashCode();
        }
    }
}