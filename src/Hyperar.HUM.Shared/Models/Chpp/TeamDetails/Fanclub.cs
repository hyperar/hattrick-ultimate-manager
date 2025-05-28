namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record Fanclub(long FanclubId, string FanclubName, int FanclubSize)
    {
        public bool Equals(Fanclub? other)
        {
            return other != null
                && this.FanclubId == other.FanclubId
                && this.FanclubName == other.FanclubName
                && this.FanclubSize == other.FanclubSize;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.FanclubId);
            hash.Add(this.FanclubName);
            hash.Add(this.FanclubSize);

            return hash.ToHashCode();
        }
    }
}