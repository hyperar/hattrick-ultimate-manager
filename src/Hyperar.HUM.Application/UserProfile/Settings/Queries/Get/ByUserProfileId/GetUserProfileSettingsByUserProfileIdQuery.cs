namespace Hyperar.HUM.Application.UserProfile.Settings.Queries.Get.ByUserProfileId
{
    using System;
    using MediatR;

    public class GetUserProfileSettingsByUserProfileIdQuery : IRequest<Domain.UserProfileSettings>
    {
        public Guid UserProfileId { get; set; }
    }
}