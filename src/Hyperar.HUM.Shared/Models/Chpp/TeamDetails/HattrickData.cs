namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public record HattrickData(
        string FileName,
        decimal Version,
        long UserId,
        DateTime FetchedDate,
        User User,
        Team[] Teams) : XmlFileBase(FileName, Version, UserId, FetchedDate);
}