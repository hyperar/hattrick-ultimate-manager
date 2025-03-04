namespace Hyperar.HUM.Shared.Models.Chpp.WorldDetails
{
    using System;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;

    public record HattrickData(
        string FileName,
        decimal Version,
        long UserId,
        DateTime FetchedDate,
        League[] LeagueList) : XmlFileBase(FileName, Version, UserId, FetchedDate), IXmlFileBase;
}