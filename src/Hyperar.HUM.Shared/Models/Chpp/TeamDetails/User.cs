namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;
    using System.Linq;

    public sealed record User(
        long UserId,
        IdName? Language,
        string SupporterTier,
        string LoginName,
        string Name,
        string ICQ,
        DateTime SignupDate,
        DateTime ActivationDate,
        DateTime LastLoginDate,
        bool HasManagerLicense,
        NationalTeam[] NationalTeams)
    {
        public bool Equals(User? other)
        {
            return other != null
                && this.UserId == other.UserId
                && this.Language == other.Language
                && this.SupporterTier == other.SupporterTier
                && this.LoginName == other.LoginName
                && this.Name == other.Name
                && this.ICQ == other.ICQ
                && this.SignupDate == other.SignupDate
                && this.ActivationDate == other.ActivationDate
                && this.LastLoginDate == other.LastLoginDate
                && this.HasManagerLicense == other.HasManagerLicense
                && this.NationalTeams.SequenceEqual(other.NationalTeams);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.UserId);
            hash.Add(this.Language);
            hash.Add(this.SupporterTier);
            hash.Add(this.LoginName);
            hash.Add(this.Name);
            hash.Add(this.ICQ);
            hash.Add(this.SignupDate);
            hash.Add(this.ActivationDate);
            hash.Add(this.LastLoginDate);
            hash.Add(this.HasManagerLicense);
            hash.Add(this.NationalTeams);

            return hash.ToHashCode();
        }
    }
}