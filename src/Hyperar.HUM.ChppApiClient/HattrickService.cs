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

    public class HattrickService : IHattrickService
    {
        private readonly string accessTokenUrl;

        private readonly string authorizationUrl;

        private readonly string callbackUrl;

        private readonly string checkTokenUrl;

        private readonly string consumerKey;

        private readonly string consumerSecret;

        private readonly string invalidateTokenUrl;

        private readonly IProtectedResourceUrlFactory protectedResourceUrlFactory;

        private readonly string requestTokenUrl;

        private readonly string userAgent;

        public HattrickService(
            string accessTokenUrl,
            string authorizationUrl,
            string callbackUrl,
            string checkTokenUrl,
            string invalidateTokenUrl,
            string requestTokenUrl,
            string consumerKey,
            string consumerSecret,
            string userAgent,
            IProtectedResourceUrlFactory protectedResourceUrlFactory)
        {
            this.accessTokenUrl = accessTokenUrl;
            this.authorizationUrl = authorizationUrl;
            this.callbackUrl = callbackUrl;
            this.checkTokenUrl = checkTokenUrl;
            this.consumerKey = consumerKey;
            this.consumerSecret = consumerSecret;
            this.invalidateTokenUrl = invalidateTokenUrl;
            this.requestTokenUrl = requestTokenUrl;
            this.userAgent = userAgent;

            this.protectedResourceUrlFactory = protectedResourceUrlFactory;
        }

        public async Task<byte[]> CheckTokenAsync(AccessToken accessToken, CancellationToken cancellationToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(accessToken.Token);
            ArgumentException.ThrowIfNullOrWhiteSpace(accessToken.Secret);

            var session = this.CreateSignedOAuthSession(accessToken.Token, accessToken.Secret);

            ArgumentException.ThrowIfNullOrEmpty(this.checkTokenUrl);

            return await GetResponseByteArrayForUrlAsync(this.checkTokenUrl, session);
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

            ArgumentException.ThrowIfNullOrEmpty(this.invalidateTokenUrl);

            var session = this.CreateSignedOAuthSession(accessToken.Token, accessToken.Secret);

            return await GetResponseStringForUrlAsync(this.invalidateTokenUrl, session);
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
            ArgumentException.ThrowIfNullOrEmpty(this.accessTokenUrl);
            ArgumentException.ThrowIfNullOrEmpty(this.authorizationUrl);
            ArgumentException.ThrowIfNullOrEmpty(this.callbackUrl);
            ArgumentException.ThrowIfNullOrEmpty(this.requestTokenUrl);
            ArgumentException.ThrowIfNullOrEmpty(this.consumerKey);
            ArgumentException.ThrowIfNullOrEmpty(this.consumerSecret);
            ArgumentException.ThrowIfNullOrEmpty(this.userAgent);

            return new OAuthSession(
                new OAuthConsumerContext
                {
                    ConsumerKey = this.consumerKey,
                    ConsumerSecret = this.consumerSecret,
                    SignatureMethod = SignatureMethod.HmacSha1,
                    UserAgent = this.userAgent
                },
                this.requestTokenUrl,
                this.authorizationUrl,
                this.accessTokenUrl,
                this.callbackUrl);
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