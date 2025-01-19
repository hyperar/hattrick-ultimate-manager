namespace Hyperar.HUM.ChppApiClient.IntegrationTest
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Hyperar.HUM.ChppApiClient.Constants;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Shared.Models.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class HattrickServiceTests : IClassFixture<ServicesFixture>
    {
        private const string AccessTokenKeyName = "OAuth:Endpoints:Base:AccessToken";

        private const string AuthorizeKeyName = "OAuth:Endpoints:Base:Authorize";

        private const string CallbackKeyName = "OAuth:Endpoints:Base:Callback";

        private const string CheckTokenKeyName = "OAuth:Endpoints:Base:CheckToken";

        private const string InvalidateTokenKeyName = "OAuth:Endpoints:Base:InvalidateToken";

        private const string RequestTokenKeyName = "OAuth:Endpoints:Base:RequestToken";

        private const string UserAgentKeyName = "OAuth:UserAgent";

        private readonly string accessTokenUrl;

        private readonly string authorizationUrl;

        private readonly string callbackUrl;

        private readonly string checkTokenUrl;

        private readonly string invalidateTokenUrl;

        private readonly IProtectedResourceUrlFactory protectedResourceUrlFactory;

        private readonly string requestTokenUrl;

        private readonly int serverPort;

        private readonly IServiceProvider services;

        private readonly string userAgent;

        public HattrickServiceTests(ServicesFixture fixture)
        {
            this.services = fixture.Services;
            this.protectedResourceUrlFactory = this.services.GetRequiredService<IProtectedResourceUrlFactory>();

            this.serverPort = WireMockServerFactory.StartServerAndGetPort();

            var configuration = this.services.GetRequiredService<IConfiguration>();

            var wireMockServer = WireMockServerFactory.StartServerAndGetPort();

            var accessTokenUrlMask = configuration[AccessTokenKeyName];
            var authorizationUrlMask = configuration[AuthorizeKeyName];
            var callbackUrlMask = configuration[CallbackKeyName];
            var checkTokenUrlMask = configuration[CheckTokenKeyName];
            var invalidateTokenUrlMask = configuration[InvalidateTokenKeyName];
            var requestTokenUrlMask = configuration[RequestTokenKeyName];

            var userAgent = configuration[UserAgentKeyName];

            ArgumentException.ThrowIfNullOrWhiteSpace(accessTokenUrlMask);
            ArgumentException.ThrowIfNullOrWhiteSpace(authorizationUrlMask);
            ArgumentException.ThrowIfNullOrWhiteSpace(callbackUrlMask);
            ArgumentException.ThrowIfNullOrWhiteSpace(checkTokenUrlMask);
            ArgumentException.ThrowIfNullOrWhiteSpace(invalidateTokenUrlMask);
            ArgumentException.ThrowIfNullOrWhiteSpace(requestTokenUrlMask);
            ArgumentException.ThrowIfNullOrWhiteSpace(userAgent);

            this.accessTokenUrl = string.Format(accessTokenUrlMask, this.serverPort);
            this.authorizationUrl = string.Format(authorizationUrlMask, this.serverPort);
            this.callbackUrl = string.Format(callbackUrlMask, this.serverPort);
            this.checkTokenUrl = string.Format(checkTokenUrlMask, this.serverPort);
            this.invalidateTokenUrl = string.Format(invalidateTokenUrlMask, this.serverPort);
            this.requestTokenUrl = string.Format(requestTokenUrlMask, this.serverPort);
            this.userAgent = userAgent;
        }

        [Fact]
        public async Task HattrickServiceCheckToken_ShouldReturnOkResponse()
        {
            var hattrickService = this.GetHattrickService(Valid.ConsumerKey, Valid.ConsumerSecret);

            var cancellationTokenSource = new CancellationTokenSource();

            var response = await hattrickService.CheckTokenAsync(
                new AccessToken(
                    Valid.AccessToken,
                    Valid.AccessSecret,
                    DateTime.Now.AddDays(-5),
                    DateTime.MaxValue),
                cancellationTokenSource.Token);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task HattrickServiceCheckToken_ShouldThrowUnauthorizedHttpRequestException()
        {
            var hattrickService = this.GetHattrickService(Valid.ConsumerKey, Valid.ConsumerSecret);

            var cancellationTokenSource = new CancellationTokenSource();

            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => hattrickService.CheckTokenAsync(
                new AccessToken(
                    Invalid.AccessToken,
                    Invalid.AccessSecret,
                    DateTime.Now.AddDays(-5),
                    DateTime.MaxValue),
                cancellationTokenSource.Token));

            Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
        }

        [Fact]
        public async Task HattrickServiceGetAccessToken_ShouldReturnOk()
        {
            var hattrickService = this.GetHattrickService(
                Valid.ConsumerKey,
                Valid.ConsumerSecret);

            var cancellationTokenSource = new CancellationTokenSource();

            var response = await hattrickService.GetAccessTokenAsync(
                Valid.VerificationCode,
                new RequestToken(
                    Valid.RequestToken,
                    Valid.RequestSecret),
                cancellationTokenSource.Token);

            Assert.NotNull(response);

            Assert.Equal(Valid.AccessToken, response.Token);
            Assert.Equal(Valid.AccessSecret, response.Secret);
        }

        [Fact]
        public async Task HattrickServiceGetAccessToken_ShouldReturnUnauthorized()
        {
            var hattrickService = this.GetHattrickService(
                Invalid.ConsumerKey,
                Invalid.ConsumerSecret);

            var cancellationTokenSource = new CancellationTokenSource();

            await Assert.ThrowsAsync<ArgumentNullException>(() => hattrickService.GetAccessTokenAsync(
                Invalid.VerificationCode,
                new RequestToken(
                    Valid.RequestToken,
                    Valid.RequestSecret),
                cancellationTokenSource.Token));
        }

        [Fact]
        public async Task HattrickServiceGetAuthorizationUrl_ShouldReturnAuthorizationUrl()
        {
            var hattrickService = this.GetHattrickService(
                Valid.ConsumerKey,
                Valid.ConsumerSecret);

            var cancellationTokenSource = new CancellationTokenSource();

            var response = await hattrickService.GetAuthorizationUrlAsync(
                new RequestToken(
                    Valid.RequestToken,
                    Valid.RequestSecret),
                cancellationTokenSource.Token);

            Assert.Equal($"{this.authorizationUrl}?oauth_token={Valid.RequestToken}", response);
        }

        [Fact]
        public async Task HattrickServiceGetRequestToken_ShouldReturnOk()
        {
            var hattrickService = this.GetHattrickService(
                Valid.ConsumerKey,
                Valid.ConsumerSecret);

            var cancellationTokenSource = new CancellationTokenSource();

            var response = await hattrickService.GetRequestTokenAsync(cancellationTokenSource.Token);

            Assert.Equal(
                response,
                new RequestToken(
                    Valid.RequestToken,
                    Valid.RequestSecret));
        }

        [Fact]
        public async Task HattrickServiceGetRequestToken_ShouldReturnUnauthorized()
        {
            var hattrickService = this.GetHattrickService(
                Invalid.ConsumerKey,
                Invalid.ConsumerSecret);

            var cancellationTokenSource = new CancellationTokenSource();

            await Assert.ThrowsAsync<ArgumentNullException>(() => hattrickService.GetRequestTokenAsync(cancellationTokenSource.Token));
        }

        [Fact]
        public async Task HattrickServiceRevokeToken_ShouldThrowUnauthorizedHttpRequestException()
        {
            var hattrickService = this.GetHattrickService(Valid.ConsumerKey, Valid.ConsumerSecret);

            var cancellationTokenSource = new CancellationTokenSource();

            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => hattrickService.RevokeTokenAsync(
                new AccessToken(
                    Invalid.AccessToken,
                    Invalid.AccessSecret,
                    DateTime.Now.AddDays(-5),
                    DateTime.MaxValue),
                cancellationTokenSource.Token));

            Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
        }

        [Fact]
        public async Task HattrickServicRevokeToken_ShouldReturnOkResponse()
        {
            var hattrickService = this.GetHattrickService(Valid.ConsumerKey, Valid.ConsumerSecret);

            var cancellationTokenSource = new CancellationTokenSource();

            var response = await hattrickService.RevokeTokenAsync(
                new AccessToken(
                    Valid.AccessToken,
                    Valid.AccessSecret,
                    DateTime.Now.AddDays(-5),
                    DateTime.MaxValue),
                cancellationTokenSource.Token);

            Assert.NotNull(response);
        }

        private HattrickService GetHattrickService(string consumerKey, string consumerSecret)
        {
            return new HattrickService(
                this.accessTokenUrl,
                this.authorizationUrl,
                this.callbackUrl,
                this.checkTokenUrl,
                this.invalidateTokenUrl,
                this.requestTokenUrl,
                consumerKey,
                consumerSecret,
                this.userAgent,
                this.protectedResourceUrlFactory);
        }
    }
}