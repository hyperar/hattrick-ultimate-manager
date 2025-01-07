namespace Hyperar.HUM.Shared.Models.Chpp
{
    using System.Collections.Generic;

    public record Avatar(string BackgroundImage, IEnumerable<Layer>? Layers);
}