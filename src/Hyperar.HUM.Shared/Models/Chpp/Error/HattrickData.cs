namespace Hyperar.HUM.Shared.Models.Chpp.Error
{
    using System;

    public sealed record HattrickData(
        string FileName,
        decimal Version,
        long UserId,
        DateTime FetchedDate,
        string Error,
        int ErrorCode,
        Guid ErrorGuid,
        string Request,
        int LineNumber) : XmlFileBase(FileName, Version, UserId, FetchedDate)
    {
        public bool Equals(HattrickData? other)
        {
            return other != null
                && this.FileName == other.FileName
                && this.Version == other.Version
                && this.UserId == other.UserId
                && this.FetchedDate == other.FetchedDate
                && this.Error == other.Error
                && this.ErrorCode == other.ErrorCode
                && this.ErrorGuid == other.ErrorGuid
                && this.Request == other.Request
                && this.LineNumber == other.LineNumber;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.FileName);
            hash.Add(this.Version);
            hash.Add(this.UserId);
            hash.Add(this.FetchedDate);
            hash.Add(this.Error);
            hash.Add(this.ErrorCode);
            hash.Add(this.ErrorGuid);
            hash.Add(this.Request);
            hash.Add(this.LineNumber);

            return hash.ToHashCode();
        }
    }
}