namespace Hyperar.HUM.Domain
{
    using System;
    using Hyperar.HUM.Shared.Enums;

    public class OAuthToken : EntityBase
    {
        public DateTime CreatedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public ChppScope Scope { get; set; }

        public string Secret { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public virtual UserProfile UserProfile { get; set; } = new UserProfile();

        public Guid UserProfileId { get; set; }
    }
}