namespace Hyperar.HUM.Application.UnitTest.ChppFile.Fakes
{
    using System;
    using System.Collections.Specialized;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Authorization;

    public class FakeHattrickService : IHattrickService
    {
        public Task<byte[]> CheckTokenAsync(AccessToken accessToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<AccessToken> GetAccessTokenAsync(string verificationCode, RequestToken requestToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAuthorizationUrlAsync(RequestToken requestToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetProtectedResourceAsync(AccessToken accessToken, XmlFileType xmlFileType, NameValueCollection? parameters, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<RequestToken> GetRequestTokenAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> RevokeTokenAsync(AccessToken accessToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}