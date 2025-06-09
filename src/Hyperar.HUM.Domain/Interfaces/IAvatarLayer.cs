namespace Hyperar.HUM.Domain.Interfaces
{
    using Hyperar.HUM.Shared.Enums;

    public interface IAvatarLayer
    {
        string ImageUrl { get; set; }

        int Index { get; set; }

        AvatarLayerType Type { get; set; }

        int XCoordinate { get; set; }

        int YCoordinate { get; set; }
    }
}