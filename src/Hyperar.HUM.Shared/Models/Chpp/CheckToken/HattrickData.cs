namespace Hyperar.HUM.Shared.Models.Chpp.CheckToken
{
    using System;
    using System.Linq;

    public sealed record HattrickData(
        string FileName,
        decimal Version,
        long UserId,
        DateTime FetchedDate,
        string Token,
        DateTime Created,
        long User,
        DateTime Expires,
        string[] ExtendedPermissions) : XmlFileBase(FileName, Version, UserId, FetchedDate)
    {
        public bool Equals(HattrickData? other)
        {
            return other != null
                && this.FileName == other.FileName
                && this.Version == other.Version
                && this.UserId == other.UserId
                && this.FetchedDate == other.FetchedDate
                && this.Token == other.Token
                && this.Created == other.Created
                && this.User == other.User
                && this.Expires == other.Expires
                && this.ExtendedPermissions.SequenceEqual(other.ExtendedPermissions);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.FileName);
            hash.Add(this.Version);
            hash.Add(this.UserId);
            hash.Add(this.FetchedDate);
            hash.Add(this.Token);
            hash.Add(this.Created);
            hash.Add(this.User);
            hash.Add(this.Expires);
            hash.Add(this.ExtendedPermissions);

            return hash.ToHashCode();
        }
    }
}