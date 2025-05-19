namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record Guestbook(int NumberOfGuestbookItems)
    {
        public bool Equals(Guestbook? other)
        {
            return other != null
                && this.NumberOfGuestbookItems == other.NumberOfGuestbookItems;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.NumberOfGuestbookItems);

            return hash.ToHashCode();
        }
    }
}