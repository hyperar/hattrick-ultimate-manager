namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.RevokeToken
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Shared.Models.Authorization;
    using MediatR;

    internal class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
    {
        private readonly IHattrickService hattrickService;

        public RevokeTokenCommandHandler(IHattrickService hattrickService)
        {
            this.hattrickService = hattrickService;
        }

        public async Task Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await this.hattrickService.RevokeTokenAsync(
                    new AccessToken(
                        request.Token,
                        request.Secret,
                        request.CreatedOn,
                        request.ExpiresOn),
                    cancellationToken);
            }
            catch (HttpRequestException httpException)
            {
                if (httpException.StatusCode != HttpStatusCode.Unauthorized)
                {
                    throw;
                }
            }
        }
    }
}