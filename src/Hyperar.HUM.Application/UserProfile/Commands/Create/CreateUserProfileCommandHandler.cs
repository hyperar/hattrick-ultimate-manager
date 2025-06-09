namespace Hyperar.HUM.Application.UserProfile.Commands.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain.Interfaces;
    using MediatR;

    internal class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, Domain.UserProfile>
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.UserProfile> userProfileRepository;

        private readonly IRepository<Domain.UserProfileSettings> userProfileSettingsRepository;

        public CreateUserProfileCommandHandler(
            IDatabaseContext databaseContext,
            IRepository<Domain.UserProfile> userProfileRepository,
            IRepository<Domain.UserProfileSettings> userProfileSettingsRepository)
        {
            this.databaseContext = databaseContext;
            this.userProfileRepository = userProfileRepository;
            this.userProfileSettingsRepository = userProfileSettingsRepository;
        }

        public async Task<Domain.UserProfile> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            Domain.UserProfile userProfile;

            try
            {
                await this.databaseContext.BeginTransactionAsync();

                userProfile = await this.userProfileRepository.InsertAsync(new Domain.UserProfile());

                await this.userProfileSettingsRepository.InsertAsync(
                    new Domain.UserProfileSettings
                    {
                        UseFramelessAvatars = true,
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

            return userProfile;
        }
    }
}