namespace Hyperar.HUM.Application.OAuthToken.RequestToken.Queries.Get.FromHattrick
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Shared.Models.Authorization;
    using MediatR;

    internal class GetRequestTokenFromHattrickQueryHandler : IRequestHandler<GetRequestTokenFromHattrickQuery, RequestToken>
    {
        private readonly IHattrickService hattrickService;

        public GetRequestTokenFromHattrickQueryHandler(IHattrickService hattrickService)
        {
            this.hattrickService = hattrickService;
        }

        public async Task<RequestToken> Handle(GetRequestTokenFromHattrickQuery request, CancellationToken cancellationToken)
        {
            return await this.hattrickService.GetRequestTokenAsync(cancellationToken);
        }
    }
}