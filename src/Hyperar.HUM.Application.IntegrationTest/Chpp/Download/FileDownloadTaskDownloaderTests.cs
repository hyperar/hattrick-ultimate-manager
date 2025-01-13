namespace Hyperar.HUM.Application.IntegrationTest.Chpp.Download
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.IntegrationTest.Chpp.Fixtures;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Shared.Models.Authorization;
    using Microsoft.Extensions.DependencyInjection;
    using WireMock.RequestBuilders;
    using WireMock.Server;

    public class FileDownloadTaskDownloaderTests : IClassFixture<FileDownloadTaskDownloaderFixture>
    {
        private readonly IServiceProvider services;
        private readonly WireMockServer wireMockServer;

        public FileDownloadTaskDownloaderTests(FileDownloadTaskDownloaderFixture fixture)
        {
            this.services = fixture.Services;

            this.wireMockServer = WireMockServer.Start(new WireMock.Settings.WireMockServerSettings()
            {
                Urls = new string[]
                {
                    "https://localhost:12345"
                }
            });

            this.ConfigureWireMockService();
        }

        private void ConfigureWireMockService()
        {
            // Valid Token => 200 OK.
            this.wireMockServer.Given(
                Request.Create()
                    .WithPath("/oauth/check_token.ashx")
                    .UsingGet()
                    .WithParam("oauth_token", Download.Constants.Token.ValidAccessToken))
                .ThenRespondWithOK();

            // Invalid Token => 401 Unauthorized.
            this.wireMockServer.Given(
                Request.Create()
                    .WithPath("/oauth/check_token.ashx")
                    .UsingGet()
                    .WithParam("oauth_token", Constants.Token.InvalidAccessToken))
                .ThenRespondWithStatusCode(401);
        }

        [Fact]
        public async Task HattrickServiceCheckToken_ShouldThrowUnauthorizedHttpRequestException()
        {
            var hattrickService = this.services.GetRequiredService<IHattrickService>();

            var cancellationTokenSource = new CancellationTokenSource();

            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => hattrickService.CheckTokenAsync(
                new AccessToken(
                    Constants.Token.InvalidAccessToken,
                    Constants.Token.InvalidAccessSecret,
                    DateTime.Now.AddDays(-5),
                    DateTime.MaxValue),
                cancellationTokenSource.Token));

            Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
        }

        [Fact]
        public async Task FileDownloadTaskCheckToken_ShouldReturnOkResponse()
        {
            var hattrickService = this.services.GetRequiredService<IHattrickService>();

            var cancellationTokenSource = new CancellationTokenSource();

            var response = await hattrickService.CheckTokenAsync(
                new AccessToken(
                    Constants.Token.ValidAccessToken,
                    Constants.Token.ValidAccessSecret,
                    DateTime.Now.AddDays(-5),
                    DateTime.MaxValue),
                cancellationTokenSource.Token);

            Assert.NotNull(response);
        }
    }
}
