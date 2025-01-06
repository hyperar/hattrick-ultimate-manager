namespace Hyperar.HUM.Domain
{
    using System;

    public class UserProfile : EntityBase
    {
        public DateTime? LastDownloadDate { get; set; }

        public virtual Manager? Manager { get; set; }

        public virtual OAuthToken? OAuthToken { get; set; }

        public long? SelectedTeamHattrickId { get; set; }
    }
}