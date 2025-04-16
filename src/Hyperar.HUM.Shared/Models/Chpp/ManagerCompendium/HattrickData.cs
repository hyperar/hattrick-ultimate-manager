namespace Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium
{
    using System;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;

    public sealed record HattrickData(
        string FileName,
        decimal Version,
        long UserId,
        DateTime FetchedDate,
        Manager Manager) : XmlFileBase(FileName, Version, UserId, FetchedDate), IXmlFileBase
    {
        public bool Equals(HattrickData? other)
        {
            return other != null
                && this.FileName == other.FileName
                && this.Version == other.Version
                && this.UserId == other.UserId
                && this.FetchedDate == other.FetchedDate
                && this.Manager == other.Manager;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.FileName);
            hash.Add(this.Version);
            hash.Add(this.UserId);
            hash.Add(this.FetchedDate);
            hash.Add(this.Manager);

            return hash.ToHashCode();
        }
    }
}