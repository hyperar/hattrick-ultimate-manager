namespace Hyperar.HUM.Domain
{
    using System;

    public class UserProfileSettings : EntityBase
    {
        public bool UseFramelessAvatars { get; set; }

        public virtual UserProfile UserProfile { get; set; } = new UserProfile();

        public Guid UserProfileId { get; set; }
    }
}