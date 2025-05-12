namespace Hyperar.HUM.Application.UserProfile.Queries.List
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Models.UserProfileSelection;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    internal class ListUserProfileQueryHandler : IRequestHandler<ListUserProfilesQuery, IEnumerable<UserProfile>>
    {
        private readonly IRepository<Domain.UserProfile> userProfileRepository;

        public ListUserProfileQueryHandler(IRepository<Domain.UserProfile> userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        public async Task<IEnumerable<UserProfile>> Handle(ListUserProfilesQuery request, CancellationToken cancellationToken)
        {
            return await this.userProfileRepository.Query()
                .Select(x => new UserProfile(
                    x.Id,
                    x.OAuthToken != null,
                    x.LastDownloadDate,
                    x.SelectedTeamHattrickId,
                    x.Manager != null ? x.Manager.HattrickId : null,
                    x.Manager != null ? x.Manager.UserName : null,
                    x.Manager != null ? x.Manager.AvatarBytes : null,
                    x.Manager != null ? x.Manager.Country.League.FlagBytes : null))
                .ToListAsync(cancellationToken);
        }
    }
}