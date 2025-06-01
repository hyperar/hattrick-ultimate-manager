namespace Hyperar.HUM.Shared.Models.Chpp.Players
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed record Team(long TeamId, string TeamName, Player[]? PlayerList)
    {
        public bool Equals(Team? other)
        {
            return other != null
                && this.TeamId == other.TeamId
                && this.TeamName == other.TeamName
                && (this.PlayerList ?? Array.Empty<Player>()).SequenceEqual(other.PlayerList ?? Array.Empty<Player>());
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.TeamId);
            hash.Add(this.TeamName);
            hash.Add(this.PlayerList);

            return hash.ToHashCode();
        }

        public IEnumerable<long> Select(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}