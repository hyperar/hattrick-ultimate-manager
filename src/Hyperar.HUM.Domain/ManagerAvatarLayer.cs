namespace Hyperar.HUM.Domain
{
    public class ManagerAvatarLayer : AvatarLayerBase
    {
        public virtual Manager Manager { get; set; } = new Manager();

        public long ManagerHattrickId { get; set; }
    }
}