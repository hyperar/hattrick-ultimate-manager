namespace Hyperar.HUM.ChppApiClient
{
    using System;
    using System.Collections.Specialized;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Authorization;
    using Hyperar.OAuthCore.Consumer;
    using Hyperar.OAuthCore.Framework;
    using Microsoft.Extensions.Configuration;

    public class HattrickService : IHattrickService
    {
        private const string AccessTokenKeyName = "OAuth:Endpoints:Base:AccessToken";

        private const string AuthorizeKeyName = "OAuth:Endpoints:Base:Authorize";

        private const string CallbackKeyName = "OAuth:Endpoints:Base:Callback";

        private const string CheckTokenKeyName = "OAuth:Endpoints:Base:CheckToken";

        private const string ConsumerKeyKeyName = "OAuth:ConsumerKey";

        private const string ConsumerSecretKeyName = "OAuth:ConsumerSecret";

        private const string InvalidateTokenKeyName = "OAuth:Endpoints:Base:InvalidateToken";

        private const string RequestTokenKeyName = "OAuth:Endpoints:Base:RequestToken";

        private const string UserAgentKeyName = "OAuth:UserAgent";

        private readonly IConfiguration configuration;

        private readonly IProtectedResourceUrlFactory protectedResourceUrlFactory;

        public HattrickService(IConfiguration configuration, IProtectedResourceUrlFactory protectedResourceUrlFactory)
        {
            this.configuration = configuration;
            this.protectedResourceUrlFactory = protectedResourceUrlFactory;
        }

        public async Task<byte[]> CheckTokenAsync(AccessToken accessToken, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(accessToken.Token);
            ArgumentException.ThrowIfNullOrWhiteSpace(accessToken.Secret);

            var session = this.CreateSignedOAuthSession(accessToken.Token, accessToken.Secret);

            var checkTokenUrl = this.configuration[CheckTokenKeyName];

            ArgumentException.ThrowIfNullOrEmpty(checkTokenUrl);

            return await GetResponseByteArrayForUrlAsync(checkTokenUrl, session);
        }

        public async Task<AccessToken> GetAccessTokenAsync(string verificationCode, RequestToken requestToken, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(verificationCode);
            ArgumentException.ThrowIfNullOrWhiteSpace(requestToken.Token);
            ArgumentException.ThrowIfNullOrWhiteSpace(requestToken.Secret);

            var session = this.CreateOAuthSession();

            ArgumentNullException.ThrowIfNull(session);

            var token = new TokenBase
            {
                Token = requestToken.Token,
                TokenSecret = requestToken.Secret
            };

            var accessToken = await session.ExchangeRequestTokenForAccessTokenAsync(
                token,
                HttpMethod.Get.ToString(),
                verificationCode);

            ArgumentException.ThrowIfNullOrWhiteSpace(accessToken.Token);
            ArgumentException.ThrowIfNullOrWhiteSpace(accessToken.TokenSecret);

            return new AccessToken(
                accessToken.Token,
                accessToken.TokenSecret,
                DateTime.Now,
                DateTime.MaxValue);
        }

        public Task<string> GetAuthorizationUrlAsync(RequestToken requestToken, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(requestToken.Token);
            ArgumentException.ThrowIfNullOrWhiteSpace(requestToken.Secret);

            var session = this.CreateOAuthSession();

            ArgumentNullException.ThrowIfNull(session);

            var url = session.GetUserAuthorizationUrlForToken(
                new TokenBase
                {
                    Token = requestToken.Token,
                    TokenSecret = requestToken.Secret
                });

            return Task.FromResult(url);
        }

        public async Task<byte[]> GetProtectedResourceAsync(
            AccessToken accessToken,
            XmlFileType fileType,
            NameValueCollection? parameters,
            CancellationToken cancellationToken)
        {
            var url = this.protectedResourceUrlFactory.BuildUrl(fileType, parameters);

            ArgumentException.ThrowIfNullOrEmpty(url);

            var session = this.CreateSignedOAuthSession(accessToken.Token, accessToken.Secret);

            ArgumentNullException.ThrowIfNull(session);

            return await GetResponseByteArrayForUrlAsync(url, session);
        }

        public async Task<RequestToken> GetRequestTokenAsync(CancellationToken cancellationToken)
        {
            var session = this.CreateOAuthSession();

            ArgumentNullException.ThrowIfNull(session);

            var requestToken = await session.GetRequestTokenAsync(HttpMethod.Get.ToString());

            ArgumentNullException.ThrowIfNull(requestToken);
            ArgumentException.ThrowIfNullOrWhiteSpace(requestToken.Token);
            ArgumentException.ThrowIfNullOrWhiteSpace(requestToken.TokenSecret);

            return new RequestToken(requestToken.Token, requestToken.TokenSecret);
        }

        public async Task<string> RevokeTokenAsync(AccessToken accessToken, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(accessToken.Token);
            ArgumentNullException.ThrowIfNull(accessToken.Secret);

            var invalidateTokenUrl = this.configuration[InvalidateTokenKeyName];

            ArgumentException.ThrowIfNullOrEmpty(invalidateTokenUrl);

            var session = this.CreateSignedOAuthSession(accessToken.Token, accessToken.Secret);

            return await GetResponseStringForUrlAsync(invalidateTokenUrl, session);
        }

        private static async Task<byte[]> GetResponseByteArrayForUrlAsync(string url, OAuthSession session)
        {
            var httpContent = await GetResponseForUrlAsync(url, session);

            return await httpContent.ReadAsByteArrayAsync();
        }

        private static async Task<HttpContent> GetResponseForUrlAsync(string url, OAuthSession session)
        {
            ArgumentException.ThrowIfNullOrEmpty(url);

            ArgumentNullException.ThrowIfNull(session);

            var responseMessage = await session.Request()
                .ForUrl(url)
                .ForMethod(HttpMethod.Get.ToString())
                .ToResponseMessageAsync();

            responseMessage.EnsureSuccessStatusCode();

            return responseMessage.Content;
        }

        private static async Task<string> GetResponseStringForUrlAsync(string url, OAuthSession session)
        {
            var httpContent = await GetResponseForUrlAsync(url, session);

            return await httpContent.ReadAsStringAsync();
        }

        private OAuthSession CreateOAuthSession()
        {
            var accessTokenUrl = this.configuration[AccessTokenKeyName];
            var authorizeUrl = this.configuration[AuthorizeKeyName];
            var callbackUrl = this.configuration[CallbackKeyName];
            var consumerKey = this.configuration[ConsumerKeyKeyName];
            var consumerSecret = this.configuration[ConsumerSecretKeyName];
            var requestTokenUrl = this.configuration[RequestTokenKeyName];
            var userAgent = this.configuration[UserAgentKeyName];

            ArgumentException.ThrowIfNullOrEmpty(accessTokenUrl);
            ArgumentException.ThrowIfNullOrEmpty(authorizeUrl);
            ArgumentException.ThrowIfNullOrEmpty(callbackUrl);
            ArgumentException.ThrowIfNullOrEmpty(consumerKey);
            ArgumentException.ThrowIfNullOrEmpty(consumerSecret);
            ArgumentException.ThrowIfNullOrEmpty(requestTokenUrl);
            ArgumentException.ThrowIfNullOrEmpty(userAgent);

            return new OAuthSession(
                new OAuthConsumerContext
                {
                    ConsumerKey = consumerKey,
                    ConsumerSecret = consumerSecret,
                    SignatureMethod = SignatureMethod.HmacSha1,
                    UserAgent = userAgent
                },
                requestTokenUrl,
                authorizeUrl,
                accessTokenUrl,
                callbackUrl);
        }

        private OAuthSession CreateSignedOAuthSession(string token, string secret)
        {
            var session = this.CreateOAuthSession();

            session.AccessToken = new TokenBase
            {
                Token = token,
                TokenSecret = secret
            };

            return session;
        }
    }
}