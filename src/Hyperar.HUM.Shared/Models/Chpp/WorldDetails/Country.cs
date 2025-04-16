namespace Hyperar.HUM.Shared.Models.Chpp.WorldDetails
{
    using System;
    using System.Linq;

    public sealed record Country(
        bool Available,
        long? CountryId,
        string? CountryName,
        string? CurrencyName,
        decimal? CurrencyRate,
        string? CountryCode,
        string? DateFormat,
        string? TimeFormat,
        IdName[]? RegionList)
    {
        public bool Equals(Country? other)
        {
            return other != null
                && this.Available == other.Available
                && this.CountryId == other.CountryId
                && this.CountryName == other.CountryName
                && this.CurrencyName == other.CurrencyName
                && this.CurrencyRate == other.CurrencyRate
                && this.CountryCode == other.CountryCode
                && this.DateFormat == other.DateFormat
                && this.TimeFormat == other.TimeFormat
                && (this.RegionList ?? Array.Empty<IdName>()).SequenceEqual(other.RegionList ?? Array.Empty<IdName>());
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.Available);
            hash.Add(this.CountryId);
            hash.Add(this.CountryName);
            hash.Add(this.CurrencyName);
            hash.Add(this.CurrencyRate);
            hash.Add(this.CountryCode);
            hash.Add(this.DateFormat);
            hash.Add(this.TimeFormat);
            hash.Add(this.RegionList);

            return hash.ToHashCode();
        }
    }
}