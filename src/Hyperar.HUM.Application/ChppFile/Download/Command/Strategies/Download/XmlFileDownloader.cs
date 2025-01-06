namespace Hyperar.HUM.Application.ChppFile.Download.Command.Strategies.Download
{
    using System;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Interfaces;
    using Hyperar.HUM.Application.ChppFile.Download.Command.Models;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Enums;
    using Hyperar.HUM.Shared.Models.Authorization;
    using Microsoft.EntityFrameworkCore;

    public abstract class XmlFileDownloader : IFileDownloaderStrategy
    {
        protected readonly IHattrickService hattrickService;

        private readonly IRepository<Domain.OAuthToken> oauthTokenRepository;

        public XmlFileDownloader(
            IRepository<Domain.OAuthToken> oauthTokenRepository,
            IHattrickService hattrickService)
        {
            this.hattrickService = hattrickService;
            this.oauthTokenRepository = oauthTokenRepository;
        }

        public async Task ExecuteFileDownloadAsync(FileDownloadTaskBase fileDownloadTask, CancellationToken cancellationToken)
        {
            var xmlFileDownloadTask = fileDownloadTask as XmlFileDownloadTask;

            ArgumentNullException.ThrowIfNull(xmlFileDownloadTask);

            var oauthToken = await this.oauthTokenRepository.Query(x => x.UserProfileId == xmlFileDownloadTask.UserProfileId)
                .SingleAsync();

            xmlFileDownloadTask.FileContent = await this.CallApiAsync(
                new AccessToken(
                    oauthToken.Token,
                    oauthToken.Secret,
                    oauthToken.CreatedOn,
                    oauthToken.ExpiresOn),
                xmlFileDownloadTask,
                cancellationToken);

            ArgumentNullException.ThrowIfNull(xmlFileDownloadTask.FileContent);

            fileDownloadTask.Status = DownloadTaskStatus.Downloaded;
        }

        protected abstract Task<byte[]> CallApiAsync(
            AccessToken accessToken,
            XmlFileDownloadTask fileDownloadTask,
            CancellationToken cancellationToken);
    }
}