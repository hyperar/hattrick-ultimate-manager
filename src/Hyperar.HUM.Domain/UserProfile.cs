namespace Hyperar.HUM.Domain
{
    using System;
    using Hyperar.HUM.Domain.Enums;

    public class UserProfile : EntityBase
    {
        public DateTime? LastDownloadDate { get; set; }

        public virtual OAuthToken? OAuthToken { get; set; }

        public long SelectedTeamHattrickId { get; set; }

        public SupporterTier SupporterTier { get; set; }
    }
}