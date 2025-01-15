namespace Hyperar.HUM.Application.IntegrationTest.Chpp.Download
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.IntegrationTest.Chpp.Fixtures;
    using Hyperar.HUM.Application.IntegrationTest.Constants;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Shared.Models.Authorization;
    using Microsoft.Extensions.DependencyInjection;

    public class FileDownloadTaskDownloaderTests : IClassFixture<FileDownloadTaskDownloaderFixture>
    {
        private readonly IServiceProvider services;

        public FileDownloadTaskDownloaderTests(FileDownloadTaskDownloaderFixture fixture)
        {
            this.services = fixture.Services;

        }

        [Fact]
        public async Task HattrickServiceCheckToken_ShouldThrowUnauthorizedHttpRequestException()
        {
            var hattrickService = this.services.GetRequiredService<IHattrickService>();

            var cancellationTokenSource = new CancellationTokenSource();

            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => hattrickService.CheckTokenAsync(
                new AccessToken(
                    OAuth.InvalidAccessToken,
                    OAuth.InvalidAccessSecret,
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
                    OAuth.ValidAccessToken,
                    OAuth.ValidAccessSecret,
                    DateTime.Now.AddDays(-5),
                    DateTime.MaxValue),
                cancellationTokenSource.Token);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task FileDownloadTaskRevokeToken_ShouldReturnOkResponse()
        {
            var hattrickService = this.services.GetRequiredService<IHattrickService>();

            var cancellationTokenSource = new CancellationTokenSource();

            var response = await hattrickService.RevokeTokenAsync(
                new AccessToken(
                    OAuth.ValidAccessToken,
                    OAuth.ValidAccessSecret,
                    DateTime.Now.AddDays(-5),
                    DateTime.MaxValue),
                cancellationTokenSource.Token);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task HattrickServiceRevokeToken_ShouldThrowUnauthorizedHttpRequestException()
        {
            var hattrickService = this.services.GetRequiredService<IHattrickService>();

            var cancellationTokenSource = new CancellationTokenSource();

            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => hattrickService.RevokeTokenAsync(
                new AccessToken(
                    OAuth.InvalidAccessToken,
                    OAuth.InvalidAccessSecret,
                    DateTime.Now.AddDays(-5),
                    DateTime.MaxValue),
                cancellationTokenSource.Token));

            Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
        }
    }
}
