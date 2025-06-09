namespace Hyperar.HUM.Application.UserProfile.Commands.UpdateSelectedTeam
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain.Interfaces;
    using MediatR;

    internal class UpdateUserProfileSelectedTeamCommandHandler : IRequestHandler<UpdateUserProfileSelectedTeamCommand>
    {
        private readonly IDatabaseContext databaseContext;

        private readonly IRepository<Domain.UserProfile> userProfileRepository;

        public UpdateUserProfileSelectedTeamCommandHandler(
            IDatabaseContext databaseContext,
            IRepository<Domain.UserProfile> userProfileRepository)
        {
            this.databaseContext = databaseContext;
            this.userProfileRepository = userProfileRepository;
        }

        public async Task Handle(UpdateUserProfileSelectedTeamCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await this.userProfileRepository.GetByIdAsync(request.UserProfileId);

            ArgumentNullException.ThrowIfNull(userProfile);

            userProfile.SelectedTeamHattrickId = request.SelectedTeamHattrickId;

            await this.databaseContext.SaveAsync();
        }
    }
}