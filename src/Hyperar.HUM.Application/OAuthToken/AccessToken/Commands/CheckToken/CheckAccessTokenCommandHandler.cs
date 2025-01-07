namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.CheckToken
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Shared.Models.Authorization;
    using MediatR;

    internal class CheckAccessTokenCommandHandler : IRequestHandler<CheckAccessTokenCommand, bool>
    {
        private readonly IHattrickService hattrickService;

        public CheckAccessTokenCommandHandler(IHattrickService hattrickService)
        {
            this.hattrickService = hattrickService;
        }

        public async Task<bool> Handle(CheckAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var response = true;

            try
            {
                var apiResponse = await this.hattrickService.CheckTokenAsync(
                    new AccessToken(
                        request.UserProfileToken.Token,
                        request.UserProfileToken.Secret,
                        request.UserProfileToken.CreatedOn,
                        request.UserProfileToken.ExpiresOn),
                    cancellationToken);
            }
            catch (HttpRequestException httpException)
            {
                if (httpException.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response = false;
                }
                else
                {
                    throw;
                }
            }

            return response;
        }
    }
}