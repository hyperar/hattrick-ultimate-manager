﻿namespace Hyperar.HUM.Shared.Models.Chpp.WorldDetails
{
    using System;
    using System.Linq;

    public sealed record HattrickData(
        string FileName,
        decimal Version,
        long UserId,
        DateTime FetchedDate,
        League[] LeagueList) : XmlFileBase(FileName, Version, UserId, FetchedDate)
    {
        public bool Equals(HattrickData? other)
        {
            return other != null
                && this.FileName == other.FileName
                && this.Version == other.Version
                && this.UserId == other.UserId
                && this.FetchedDate == other.FetchedDate
                && this.LeagueList.SequenceEqual(other.LeagueList);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.FileName);
            hash.Add(this.Version);
            hash.Add(this.UserId);
            hash.Add(this.FetchedDate);
            hash.Add(this.LeagueList);

            return hash.ToHashCode();
        }
    }
}