namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;

    public record HattrickData(
        string FileName,
        decimal Version,
        long UserId,
        DateTime FetchedDate,
        User User,
        Team[] Teams) : XmlFileBase(FileName, Version, UserId, FetchedDate), IXmlFileBase;
}