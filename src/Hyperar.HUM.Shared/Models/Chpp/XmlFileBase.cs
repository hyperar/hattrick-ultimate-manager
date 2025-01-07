namespace Hyperar.HUM.Shared.Models.Chpp
{
    using System;
    using Hyperar.HUM.Shared.Models.Chpp.Interfaces;

    public abstract record XmlFileBase(string FileName, decimal Version, long UserId, DateTime FetchedDate) : IXmlFileBase;
}