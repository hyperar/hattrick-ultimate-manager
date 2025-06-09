namespace Hyperar.HUM.Domain
{
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Enums;

    public abstract class AvatarLayerBase : EntityBase, IAvatarLayer
    {
        public string ImageUrl { get; set; } = string.Empty;

        public int Index { get; set; }

        public AvatarLayerType Type { get; set; }

        public int XCoordinate { get; set; }

        public int YCoordinate { get; set; }
    }
}