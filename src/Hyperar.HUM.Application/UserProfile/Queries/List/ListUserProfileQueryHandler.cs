namespace Hyperar.HUM.Application.UserProfile.Queries.List
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Models;
    using Hyperar.HUM.Shared.Models.UserProfileSelection;
    using MediatR;

    internal class ListUserProfileQueryHandler : IRequestHandler<ListUserProfilesQuery, IEnumerable<UserProfile>>
    {
        private readonly IRepository<Domain.UserProfile> userProfileRepository;

        public ListUserProfileQueryHandler(IRepository<Domain.UserProfile> userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        public async Task<IEnumerable<UserProfile>> Handle(ListUserProfilesQuery request, CancellationToken cancellationToken)
        {
            var userProfiles = new List<UserProfile>();

            foreach (var curUserProfile in this.userProfileRepository.Query().ToList())
            {
                IdName? manager = null;
                IdName? country = null;
                byte[]? avatarBytes = null;

                if (curUserProfile.Manager != null)
                {
                    manager = new IdName(curUserProfile.Manager.HattrickId, curUserProfile.Manager.UserName);
                    country = new IdName(curUserProfile.Manager.Country.HattrickId, curUserProfile.Manager.Country.Name);

                    if (curUserProfile.Manager.AvatarLayers != null)
                    {
                        avatarBytes = await ImageHelper.GetAvatarBytesAsync(
                            curUserProfile.Manager.AvatarLayers.ToArray(),
                            true,
                            cancellationToken);
                    }
                }

                userProfiles.Add(
                    new UserProfile(
                        curUserProfile.Id,
                        curUserProfile.OAuthToken != null,
                        curUserProfile.LastDownloadDate,
                        curUserProfile.SelectedTeamHattrickId,
                        manager,
                        country,
                        avatarBytes,
                        curUserProfile.Manager?.Country.League.FlagBytes));
            }

            return userProfiles.ToList();
        }
    }
}