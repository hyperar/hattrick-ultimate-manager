namespace Hyperar.HUM.Shared.Models.Chpp.Avatars
{
    using System;

    public sealed record Player(long PlayerId, Avatar Avatar)
    {
        public bool Equals(Player? other)
        {
            return other != null
                && this.PlayerId == other.PlayerId
                && this.Avatar == other.Avatar;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.PlayerId);
            hash.Add(this.Avatar);

            return hash.ToHashCode();
        }
    }
}