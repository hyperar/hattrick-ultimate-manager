namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public sealed record PressAnnouncement(DateTime SendDate, string? Subject, string? Body)
    {
        public bool Equals(PressAnnouncement? other)
        {
            return other != null
                && this.SendDate == other.SendDate
                && this.Subject == other.Subject
                && this.Body == other.Body;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.SendDate);
            hash.Add(this.Subject);
            hash.Add(this.Body);

            return hash.ToHashCode();
        }
    }
}