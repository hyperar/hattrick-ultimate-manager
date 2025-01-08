namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Download
{
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Domain;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Models.Authorization;

    public class ProtectedResourceDownloader : XmlFileDownloader
    {
        public ProtectedResourceDownloader(
            IRepository<OAuthToken> oauthTokenRepository,
            IHattrickService hattrickService) : base(oauthTokenRepository, hattrickService)
        {
        }

        protected override async Task<byte[]> CallApiAsync(
            AccessToken accessToken,
            XmlFileDownloadTask fileDownloadTask,
            CancellationToken cancellationToken)
        {
            return await this.hattrickService.GetProtectedResourceAsync(
                accessToken,
                fileDownloadTask.XmlFile,
                fileDownloadTask.Parameters,
                cancellationToken);
        }
    }
}