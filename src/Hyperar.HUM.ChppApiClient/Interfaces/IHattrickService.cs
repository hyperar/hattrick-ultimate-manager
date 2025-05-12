namespace Hyperar.HUM.ChppApiClient.Interfaces
{
    using System.Collections.Specialized;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Authorization;

    public interface IHattrickService
    {
        Task<byte[]> CheckTokenAsync(AccessToken accessToken, CancellationToken cancellationToken);

        Task<AccessToken> GetAccessTokenAsync(string verificationCode, RequestToken requestToken, CancellationToken cancellationToken);

        Task<string> GetAuthorizationUrlAsync(RequestToken requestToken, CancellationToken cancellationToken);

        Task<byte[]> GetProtectedResourceAsync(
            AccessToken accessToken,
            XmlFileType xmlFileType,
            NameValueCollection? parameters,
            CancellationToken cancellationToken);

        Task<RequestToken> GetRequestTokenAsync(CancellationToken cancellationToken);

        Task<string> RevokeTokenAsync(AccessToken accessToken, CancellationToken cancellationToken);
    }
}