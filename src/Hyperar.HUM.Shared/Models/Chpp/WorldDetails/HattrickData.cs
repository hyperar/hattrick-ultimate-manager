namespace Hyperar.HUM.Shared.Models.Chpp.WorldDetails
{
    using System;
    using System.Collections.Generic;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;

    public record HattrickData(
        string FileName,
        decimal Version,
        long UserId,
        DateTime FetchedDate,
        IEnumerable<League> LeagueList) : XmlFileBase(FileName, Version, UserId, FetchedDate), IXmlFileBase;
}