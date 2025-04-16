namespace Hyperar.HUM.Shared.Models.Chpp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed record Avatar(string BackgroundImage, IEnumerable<Layer>? Layers)
    {
        public bool Equals(Avatar? other)
        {
            return other != null
                && this.BackgroundImage == other.BackgroundImage
                && (this.Layers ?? Array.Empty<Layer>()).SequenceEqual(other.Layers ?? Array.Empty<Layer>());
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.BackgroundImage);
            hash.Add(this.Layers);

            return hash.ToHashCode();
        }
    }
}