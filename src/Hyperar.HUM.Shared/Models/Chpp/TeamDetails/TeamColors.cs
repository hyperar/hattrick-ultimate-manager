namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record TeamColors(string BackgroundColor, string Color)
    {
        public bool Equals(TeamColors? other)
        {
            return other != null
                && this.BackgroundColor == other.BackgroundColor
                && this.Color == other.Color;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.BackgroundColor);
            hash.Add(this.Color);

            return hash.ToHashCode();
        }
    }
}