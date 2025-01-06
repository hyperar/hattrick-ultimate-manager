namespace Hyperar.HUM.Application.OAuthToken.AuthorizationUrl.Queries.Get.ForRequestToken
{
    using FluentValidation;

    internal class GetAuthorizationUrlForRequestTokenQueryValidator : AbstractValidator<GetAuthorizationUrlForRequestTokenQuery>
    {
        public GetAuthorizationUrlForRequestTokenQueryValidator()
        {
            this.RuleFor(x => x.RequestToken).NotNull();
            this.RuleFor(x => x.RequestToken.Token).NotNull().NotEmpty();
            this.RuleFor(x => x.RequestToken.Secret).NotNull().NotEmpty();
        }
    }
}