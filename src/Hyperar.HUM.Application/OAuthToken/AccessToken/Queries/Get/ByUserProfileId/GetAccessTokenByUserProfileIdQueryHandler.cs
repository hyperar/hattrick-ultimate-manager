namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Queries.Get.ByUserProfileId
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Models.Authorization;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    internal class GetAccessTokenByUserProfileIdQueryHandler : IRequestHandler<GetAccessTokenByUserProfileIdQuery, UserProfileToken?>
    {
        private readonly IRepository<Domain.OAuthToken> oauthTokenRepository;

        public GetAccessTokenByUserProfileIdQueryHandler(IRepository<Domain.OAuthToken> oauthTokenRepository)
        {
            this.oauthTokenRepository = oauthTokenRepository;
        }

        public async Task<UserProfileToken?> Handle(GetAccessTokenByUserProfileIdQuery request, CancellationToken cancellationToken)
        {
            UserProfileToken? response = null;

            var oauthToken = await this.oauthTokenRepository.Query(x => x.UserProfileId == request.UserProfileId)
                .SingleOrDefaultAsync(cancellationToken);

            if (oauthToken is not null)
            {
                response = new UserProfileToken(
                    oauthToken.Id,
                    oauthToken.Token,
                    oauthToken.Secret,
                    oauthToken.Scope,
                    oauthToken.CreatedOn,
                    oauthToken.ExpiresOn);
            }

            return response;
        }
    }
}