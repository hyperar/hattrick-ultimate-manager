namespace Hyperar.HUM.Shared.Models.Chpp
{
    using System;

    public sealed record Layer(int X, int Y, string Image)
    {
        public bool Equals(Layer? other)
        {
            return other != null
                && this.X == other.X
                && this.Y == other.Y
                && this.Image == other.Image;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();

            hash.Add(this.X);
            hash.Add(this.Y);
            hash.Add(this.Image);

            return hash.ToHashCode();
        }
    }
}