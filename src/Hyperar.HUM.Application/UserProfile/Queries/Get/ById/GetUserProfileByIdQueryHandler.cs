namespace Hyperar.HUM.Application.UserProfile.Queries.Get.ById
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Models.UserProfileSelection;
    using MediatR;

    internal class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, UserProfile>
    {
        private readonly IRepository<Domain.UserProfile> userProfileRepository;

        public GetUserProfileByIdQueryHandler(IRepository<Domain.UserProfile> userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        public async Task<UserProfile> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await this.userProfileRepository.GetByIdAsync(request.Id);

            ArgumentNullException.ThrowIfNull(userProfile);

            return new UserProfile(
                userProfile.Id,
                userProfile.OAuthToken is not null,
                userProfile.LastDownloadDate,
                userProfile.SelectedTeamHattrickId,
                userProfile.Manager?.HattrickId,
                userProfile.Manager?.UserName,
                userProfile.Manager?.AvatarBytes,
                userProfile.Manager?.Country.League.FlagBytes);
        }
    }
}