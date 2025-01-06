namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Queries.Get.FromHattrick
{
    using FluentValidation;

    internal class GetAccessTokenFromHattrickQueryValidator : AbstractValidator<GetAccessTokenFromHattrickQuery>
    {
        public GetAccessTokenFromHattrickQueryValidator()
        {
            this.RuleFor(x => x.VerificationCode).NotNull().NotEmpty();
            this.RuleFor(x => x.RequestToken).NotNull();
            this.RuleFor(x => x.RequestToken.Token).NotNull().NotEmpty();
            this.RuleFor(x => x.RequestToken.Secret).NotNull().NotEmpty();
        }
    }
}