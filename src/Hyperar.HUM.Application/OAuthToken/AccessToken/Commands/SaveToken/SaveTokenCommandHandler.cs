namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.SaveToken
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Enums;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    internal class SaveTokenCommandHandler : IRequestHandler<SaveTokenCommand>
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.OAuthToken> oauthTokenRepository;

        private readonly IRepository<Domain.UserProfile> userProfileRepository;

        public SaveTokenCommandHandler(
            IDatabaseContext databaseContext,
            IRepository<Domain.OAuthToken> oauthTokenRepository,
            IRepository<Domain.UserProfile> userProfileRepository)
        {
            this.databaseContext = databaseContext;
            this.oauthTokenRepository = oauthTokenRepository;
            this.userProfileRepository = userProfileRepository;
        }

        public async Task Handle(SaveTokenCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await this.userProfileRepository.GetByIdAsync(request.UserProfileId);

            ArgumentNullException.ThrowIfNull(userProfile);

            try
            {
                await this.databaseContext.BeginTransactionAsync();

                var tokenIdsToDelete = await this.oauthTokenRepository.Query(x => x.UserProfileId == request.UserProfileId)
                    .Select(x => x.Id)
                    .ToListAsync(cancellationToken);

                await this.oauthTokenRepository.DeleteRangeAsync(tokenIdsToDelete);

                await this.oauthTokenRepository.InsertAsync(
                    new Domain.OAuthToken
                    {
                        CreatedOn = request.AccessToken.CreatedOn,
                        ExpiresOn = request.AccessToken.ExpiresOn,
                        Token = request.AccessToken.Token,
                        Scope = ChppScope.ReadOnly,
                        Secret = request.AccessToken.Secret,
                        UserProfile = userProfile
                    });
            }
            catch
            {
                throw;
            }
            finally
            {
                await this.databaseContext.EndTransactionAsync();
            }
        }
    }
}