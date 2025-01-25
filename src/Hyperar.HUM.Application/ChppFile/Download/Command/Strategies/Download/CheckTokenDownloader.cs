namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Download
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Models.Authorization;

    public class CheckTokenDownloader : XmlFileDownloader
    {
        public CheckTokenDownloader(
            IRepository<Domain.OAuthToken> oauthTokenRepository,
            IHattrickService hattrickService) : base(oauthTokenRepository, hattrickService)
        {
        }

        protected override async Task<byte[]> CallApiAsync(
            AccessToken accessToken,
            XmlFileDownloadTask fileDownloadTask,
            CancellationToken cancellationToken)
        {
            return await this.hattrickService.CheckTokenAsync(accessToken, cancellationToken);
        }
    }
}