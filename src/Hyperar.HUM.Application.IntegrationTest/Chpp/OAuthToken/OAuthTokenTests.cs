namespace Hyperar.HUM.Application.IntegrationTest.Chpp.OAuthToken
{
    using System;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.IntegrationTest.Chpp.Fixtures;
    using Hyperar.HUM.Application.OAuthToken.RequestToken.Queries.Get.FromHattrick;
    using Hyperar.HUM.Shared.Models.Authorization;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public class OAuthTokenTests : IClassFixture<FileDownloadTaskDownloaderFixture>
    {
        private readonly IServiceProvider services;

        public OAuthTokenTests(FileDownloadTaskDownloaderFixture fixture)
        {
            this.services = fixture.Services;
        }

        [Fact]
        public async Task OAuthTokenGetRequestToken_ShouldReturnOk()
        {
            var sender = this.services.GetRequiredService<ISender>();

            var response = await sender.Send(
                new GetRequestTokenFromHattrickQuery());

            Assert.Equal(
                response,
                new RequestToken(
                    Constants.OAuth.ValidRequestToken,
                    Constants.OAuth.ValidRequestSecret));
        }
    }
}
