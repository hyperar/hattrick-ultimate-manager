namespace Hyperar.HUM.Domain
{
    using System;
    using Hyperar.HUM.Domain.Enums;

    public class OAuthToken : EntityBase
    {
        public new DateTime CreatedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public string Key { get; set; } = string.Empty;

        public ChppScope Scope { get; set; }

        public string Secret { get; set; } = string.Empty;

        public virtual UserProfile UserProfile { get; set; } = new UserProfile();

        public Guid UserProfileId { get; set; }
    }
}