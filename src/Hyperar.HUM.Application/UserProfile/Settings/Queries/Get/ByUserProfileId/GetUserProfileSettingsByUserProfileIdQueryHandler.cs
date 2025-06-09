namespace Hyperar.HUM.Application.UserProfile.Settings.Queries.Get.ByUserProfileId
{
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain.Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    internal class GetUserProfileSettingsByUserProfileIdQueryHandler : IRequestHandler<GetUserProfileSettingsByUserProfileIdQuery, Domain.UserProfileSettings>
    {
        private readonly IRepository<Domain.UserProfileSettings> userProfileSettingsRepository;

        public GetUserProfileSettingsByUserProfileIdQueryHandler(IRepository<Domain.UserProfileSettings> userProfileSettingsRepository)
        {
            this.userProfileSettingsRepository = userProfileSettingsRepository;
        }

        public async Task<Domain.UserProfileSettings> Handle(GetUserProfileSettingsByUserProfileIdQuery request, CancellationToken cancellationToken)
        {
            return await this.userProfileSettingsRepository.Query(x => x.UserProfileId == request.UserProfileId)
                .SingleAsync();
        }
    }
}