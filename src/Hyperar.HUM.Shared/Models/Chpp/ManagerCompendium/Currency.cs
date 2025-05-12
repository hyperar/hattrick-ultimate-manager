namespace Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium
{
    using System;

    public sealed record Currency(string CurrencyName, decimal CurrencyRate)
    {
        public bool Equals(Currency? other)
        {
            return other != null
                && this.CurrencyName == other.CurrencyName
                && this.CurrencyRate == other.CurrencyRate;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.CurrencyName);
            hash.Add(this.CurrencyRate);

            return hash.ToHashCode();
        }
    }
}