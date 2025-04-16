namespace Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium
{
    using System;
    using System.Linq;

    public sealed record Manager(
        long UserId,
        string LoginName,
        string SupporterTier,
        string[] LastLogins,
        IdName Language,
        IdName Country,
        Currency Currency,
        Team[] Teams,
        IdName[]? NationalTeamCoach,
        IdName[]? NationalTeamAssistant,
        Avatar? Avatar)
    {
        public bool Equals(Manager? other)
        {
            return other != null
                && this.UserId == other.UserId
                && this.LoginName == other.LoginName
                && this.SupporterTier == other.SupporterTier
                && this.LastLogins.SequenceEqual(other.LastLogins)
                && this.Language == other.Language
                && this.Country == other.Country
                && this.Currency == other.Currency
                && this.Teams.SequenceEqual(other.Teams)
                && (this.NationalTeamCoach ?? Array.Empty<IdName>()).SequenceEqual(other.NationalTeamCoach ?? Array.Empty<IdName>())
                && (this.NationalTeamAssistant ?? Array.Empty<IdName>()).SequenceEqual(other.NationalTeamAssistant ?? Array.Empty<IdName>())
                && this.Avatar == other.Avatar;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.UserId);
            hash.Add(this.LoginName);
            hash.Add(this.SupporterTier);
            hash.Add(this.LastLogins);
            hash.Add(this.Language);
            hash.Add(this.Country);
            hash.Add(this.Currency);
            hash.Add(this.Teams);
            hash.Add(this.NationalTeamCoach);
            hash.Add(this.NationalTeamAssistant);
            hash.Add(this.Avatar);

            return hash.ToHashCode();
        }
    }
}