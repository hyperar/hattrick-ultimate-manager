namespace Hyperar.HUM.Domain
{
    public class SeniorPlayerAvatarLayer : AvatarLayerBase
    {
        public virtual SeniorPlayer SeniorPlayer { get; set; } = new SeniorPlayer();

        public long SeniorPlayerHattrickId { get; set; }
    }
}