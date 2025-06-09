namespace Hyperar.HUM.Application.UserProfile.Queries.Get.ById
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Models;
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

            IdName? manager = null;
            IdName? country = null;
            byte[]? avatarBytes = null;

            if (userProfile.Manager != null)
            {
                manager = new IdName(userProfile.Manager.HattrickId, userProfile.Manager.UserName);
                country = new IdName(userProfile.Manager.Country.HattrickId, userProfile.Manager.Country.Name);

                if (userProfile.Manager.AvatarLayers != null)
                {
                    avatarBytes = await ImageHelper.GetAvatarBytesAsync(
                        userProfile.Manager.AvatarLayers.ToArray(),
                        request.UseFramelessAvatar,
                        cancellationToken);
                }
            }

            return new UserProfile(
                userProfile.Id,
                userProfile.OAuthToken is not null,
                userProfile.LastDownloadDate,
                userProfile.SelectedTeamHattrickId,
                manager,
                country,
                avatarBytes,
                userProfile.Manager?.Country.League.FlagBytes);
        }
    }
}