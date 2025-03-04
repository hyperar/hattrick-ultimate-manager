namespace Hyperar.HUM.Shared.Models.Chpp.WorldDetails
{
    public record Country(
        bool Available,
        long? CountryId,
        string? CountryName,
        string? CurrencyName,
        decimal? CurrencyRate,
        string? CountryCode,
        string? DateFormat,
        string? TimeFormat,
        IdName[]? RegionList);
}