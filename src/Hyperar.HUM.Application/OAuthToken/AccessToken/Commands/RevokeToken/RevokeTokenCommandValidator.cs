namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.RevokeToken
{
    using FluentValidation;

    internal class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
    {
        public RevokeTokenCommandValidator()
        {
            this.RuleFor(x => x.Token).NotNull().NotEmpty();
            this.RuleFor(x => x.Secret).NotNull().NotEmpty();
        }
    }
}