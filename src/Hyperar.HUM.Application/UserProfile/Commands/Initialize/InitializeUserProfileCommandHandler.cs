namespace Hyperar.HUM.Application.UserProfile.Commands.Initialize
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.ChppApiClient.Interfaces;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Models.Authorization;
    using MediatR;

    internal class InitializeUserProfileCommandHandler : IRequestHandler<InitializeUserProfileCommand>
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IHattrickService hattrickService;

        private readonly IHattrickRepository<Domain.Manager> managerRepository;

        private readonly IRepository<Domain.UserProfile> userProfileRepository;

        public InitializeUserProfileCommandHandler(
            IDatabaseContext databaseContext,
            IHattrickRepository<Domain.Manager> managerRepository,
            IRepository<Domain.UserProfile> userProfileRepository,
            IHattrickService hattrickService)
        {
            this.databaseContext = databaseContext;
            this.managerRepository = managerRepository;
            this.userProfileRepository = userProfileRepository;
            this.hattrickService = hattrickService;
        }

        public async Task Handle(InitializeUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await this.userProfileRepository.GetByIdAsync(request.UserProfileId);

            ArgumentNullException.ThrowIfNull(userProfile);
            ArgumentNullException.ThrowIfNull(userProfile.OAuthToken);

            var accessToken = new AccessToken(
                userProfile.OAuthToken.Token,
                userProfile.OAuthToken.Secret,
                userProfile.OAuthToken.CreatedOn,
                userProfile.OAuthToken.ExpiresOn);

            var xmlFile = await this.hattrickService.GetProtectedResourceAsync(
                accessToken,
                Shared.Enums.XmlFileType.ManagerCompendium,
                null,
                cancellationToken);
        }
    }
}