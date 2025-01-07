namespace Hyperar.HUM.Shared.Models.Chpp.WorldDetails
{
    using System.Collections.Generic;

    public record Country(
        bool Available,
        long? CountryId,
        string? CountryName,
        string? CurrencyName,
        decimal? CurrencyRate,
        string? CountryCode,
        string? DateFormat,
        string? TimeFormat,
        IEnumerable<IdName>? RegionList);
}