namespace Hyperar.HUM.Application.OAuthToken.AuthorizationUrl.Queries.Get.ForRequestToken
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using MediatR;

    internal class GetAuthorizationUrlForRequestTokenQueryHandler : IRequestHandler<GetAuthorizationUrlForRequestTokenQuery, string>
    {
        private readonly IHattrickService hattrickService;

        public GetAuthorizationUrlForRequestTokenQueryHandler(IHattrickService hattrickService)
        {
            this.hattrickService = hattrickService;
        }

        public async Task<string> Handle(GetAuthorizationUrlForRequestTokenQuery request, CancellationToken cancellationToken)
        {
            return await this.hattrickService.GetAuthorizationUrlAsync(request.RequestToken, cancellationToken);
        }
    }
}