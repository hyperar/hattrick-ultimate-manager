namespace Hyperar.HUM.Shared.Models.Chpp.ManagerCompendium
{
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;

    public record HattrickData(
        string FileName,
        decimal Version,
        long UserId,
        DateTime FetchedDate,
        Manager Manager) : XmlFileBase(FileName, Version, UserId, FetchedDate), IXmlFileBase;
}