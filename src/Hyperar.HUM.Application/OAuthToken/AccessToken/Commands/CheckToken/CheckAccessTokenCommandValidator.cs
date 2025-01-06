namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.CheckToken
{
    using FluentValidation;

    internal class CheckAccessTokenCommandValidator : AbstractValidator<CheckAccessTokenCommand>
    {
        public CheckAccessTokenCommandValidator()
        {
            this.RuleFor(x => x.UserProfileToken.Id).NotEmpty();
            this.RuleFor(x => x.UserProfileToken.Token).NotNull().NotEmpty();
            this.RuleFor(x => x.UserProfileToken.Secret).NotNull().NotEmpty();
        }
    }
}