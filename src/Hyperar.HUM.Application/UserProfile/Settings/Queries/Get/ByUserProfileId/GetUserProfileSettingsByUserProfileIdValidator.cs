namespace Hyperar.HUM.Application.UserProfile.Settings.Queries.Get.ByUserProfileId
{
    using FluentValidation;

    internal class GetUserProfileSettingsByUserProfileIdValidator : AbstractValidator<GetUserProfileSettingsByUserProfileIdQuery>
    {
        public GetUserProfileSettingsByUserProfileIdValidator()
        {
            this.RuleFor(x => x.UserProfileId).NotEmpty();
        }
    }
}