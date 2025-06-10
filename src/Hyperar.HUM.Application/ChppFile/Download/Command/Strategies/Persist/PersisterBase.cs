namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist
{
    using System;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Persist.Constants;
    using Hyperar.HUM.Shared.Enums;

    public abstract class PersisterBase
    {
        protected const string Backgrounds = "backgrounds";

        protected const string Beards = "beards";

        protected const string Bodies = "bodies";

        protected const string Eyes = "eyes";

        protected const string Faces = "faces";

        protected const string Goatees = "goatees";

        protected const string Hairs = "hair";

        protected const string Kits = "kits";

        protected const string Miscellaneous = "misc";

        protected const string Mouths = "mouths";

        protected const string Noses = "noses";

        protected const string Numbers = "numbers";

        protected static AvatarLayerType GetAvatarLayerType(string imageUrl)
        {
            if (imageUrl.Contains(Kits))
            {
                return AvatarLayerType.Bodies;
            }
            else
            {
                var value = imageUrl.Split("/", StringSplitOptions.RemoveEmptyEntries)[2];

                return value switch
                {
                    Backgrounds => AvatarLayerType.Backgrounds,
                    Beards => AvatarLayerType.Beards,
                    Bodies => AvatarLayerType.Bodies,
                    Eyes => AvatarLayerType.Eyes,
                    Faces => AvatarLayerType.Faces,
                    Goatees => AvatarLayerType.Goatees,
                    Hairs => AvatarLayerType.Hairs,
                    Miscellaneous => AvatarLayerType.Miscellaneous,
                    Mouths => AvatarLayerType.Mouths,
                    Noses => AvatarLayerType.Noses,
                    Numbers => AvatarLayerType.Numbers,
                    _ => throw new ArgumentOutOfRangeException(value)
                };
            }
        }

        protected static SupporterTier GetSupporterTier(string value)
        {
            return value switch
            {
                SupporterTierName.None => SupporterTier.None,
                SupporterTierName.Silver => SupporterTier.Silver,
                SupporterTierName.Gold => SupporterTier.Gold,
                SupporterTierName.Platinum => SupporterTier.Platinum,
                SupporterTierName.Diamond => SupporterTier.Diamond,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }
    }
}