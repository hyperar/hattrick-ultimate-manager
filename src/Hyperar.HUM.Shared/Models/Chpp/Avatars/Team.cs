namespace Hyperar.HUM.Shared.Models.Chpp.Avatars
{
    using System;
    using System.Linq;

    public sealed record Team(long TeamId, Player[] Players)
    {
        public bool Equals(Team? other)
        {
            return other != null
                && this.TeamId == other.TeamId
                && this.Players.SequenceEqual(other.Players);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.TeamId);
            hash.Add(this.Players);

            return hash.ToHashCode();
        }
    }
}