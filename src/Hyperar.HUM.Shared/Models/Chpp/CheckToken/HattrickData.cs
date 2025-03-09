namespace Hyperar.HUM.Shared.Models.Chpp.CheckToken
{
    using System;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;

    public record HattrickData(
        string FileName,
        decimal Version,
        long UserId,
        DateTime FetchedDate,
        string Token,
        DateTime Created,
        long User,
        DateTime Expires,
        string[] ExtendedPermissions) : XmlFileBase(FileName, Version, UserId, FetchedDate), IXmlFileBase;
}