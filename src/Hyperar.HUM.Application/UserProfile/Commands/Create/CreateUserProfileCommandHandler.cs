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

        public CreateUserProfileCommandHandler(
            IDatabaseContext databaseContext,
            IRepository<Domain.UserProfile> userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
            this.databaseContext = databaseContext;
        }

        public async Task<Domain.UserProfile> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            Domain.UserProfile userProfile;

            try
            {
                await this.databaseContext.BeginTransactionAsync();

                userProfile = await this.userProfileRepository.InsertAsync(new Domain.UserProfile());
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