namespace Hyperar.HUM.Shared.Models.Chpp.Interfaces
{
    using System;

    public interface IXmlFileBase
    {
        DateTime FetchedDate { get; }

        string FileName { get; }

        long UserId { get; }

        decimal Version { get; }
    }
}