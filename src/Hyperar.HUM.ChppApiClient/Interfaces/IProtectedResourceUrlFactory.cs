namespace Hyperar.HUM.ChppApiClient.Interfaces
{
    using System.Collections.Specialized;
    using Hyperar.HUM.Shared.Enums;

    public interface IProtectedResourceUrlFactory
    {
        string BuildUrl(XmlFileType fileType, NameValueCollection? parameters);
    }
}