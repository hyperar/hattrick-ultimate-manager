namespace Hyperar.HUM.Shared.Models.Chpp.Error
{
    using System;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;

    public record HattrickData(
        string FileName,
        decimal Version,
        long UserId,
        DateTime FetchedDate,
        string Error,
        int ErrorCode,
        Guid ErrorGuid,
        string Request,
        int LineNumber) : XmlFileBase(FileName, Version, UserId, FetchedDate), IXmlFileBase;
}