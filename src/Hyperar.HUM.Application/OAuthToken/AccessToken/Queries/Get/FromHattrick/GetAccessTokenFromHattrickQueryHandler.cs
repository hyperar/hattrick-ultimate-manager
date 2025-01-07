namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Queries.Get.FromHattrick
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Shared.Models.Authorization;
    using MediatR;

    internal class GetAccessTokenFromHattrickQueryHandler : IRequestHandler<GetAccessTokenFromHattrickQuery, AccessToken>
    {
        private readonly IHattrickService hattrickService;

        public GetAccessTokenFromHattrickQueryHandler(IHattrickService hattrickService)
        {
            this.hattrickService = hattrickService;
        }

        public async Task<AccessToken> Handle(GetAccessTokenFromHattrickQuery request, CancellationToken cancellationToken)
        {
            return await this.hattrickService.GetAccessTokenAsync(
                request.VerificationCode,
                request.RequestToken,
                cancellationToken);
        }
    }
}