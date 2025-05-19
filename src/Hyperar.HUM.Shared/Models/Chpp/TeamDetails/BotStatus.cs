namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record BotStatus(bool IsBot, DateTime? BotSince)
    {
        public bool Equals(BotStatus? other)
        {
            return other != null
                && this.IsBot == other.IsBot
                && this.BotSince == other.BotSince;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.IsBot);
            hash.Add(this.BotSince);

            return hash.ToHashCode();
        }
    }
}