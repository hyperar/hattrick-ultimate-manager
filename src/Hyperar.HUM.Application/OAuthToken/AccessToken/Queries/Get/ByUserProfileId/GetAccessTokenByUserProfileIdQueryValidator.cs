namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Queries.Get.ByUserProfileId
{
    using FluentValidation;

    internal class GetAccessTokenByUserProfileIdQueryValidator : AbstractValidator<GetAccessTokenByUserProfileIdQuery>
    {
        public GetAccessTokenByUserProfileIdQueryValidator()
        {
            this.RuleFor(x => x.UserProfileId).NotEmpty();
        }
    }
}