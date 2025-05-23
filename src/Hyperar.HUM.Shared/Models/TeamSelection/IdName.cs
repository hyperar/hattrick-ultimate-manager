namespace Hyperar.HUM.Shared.Models.TeamSelection
{
    public record IdName(long HattrickId, string Name)
    {
        public override string ToString()
        {
            return $"{this.Name} ({this.HattrickId})";
        }
    }
}