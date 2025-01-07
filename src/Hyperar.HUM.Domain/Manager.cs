namespace Hyperar.HUM.Domain
{
    using System;
    using Hyperar.HUM.Shared.Enums;

    public class Manager : HattrickEntityBase
    {
        public byte[]? AvatarBytes { get; set; }

        public virtual Country Country { get; set; } = new Country();

        public long CountryHattrickId { get; set; }

        public string CurrencyName { get; set; } = string.Empty;

        public decimal CurrencyRate { get; set; }

        public SupporterTier SupporterTier { get; set; }

        public string UserName { get; set; } = string.Empty;

        public virtual UserProfile UserProfile { get; set; } = new UserProfile();

        public Guid UserProfileId { get; set; }
    }
}