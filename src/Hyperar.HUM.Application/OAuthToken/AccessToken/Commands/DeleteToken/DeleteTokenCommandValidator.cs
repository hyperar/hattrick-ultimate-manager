namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.DeleteToken
{
    using FluentValidation;

    internal class DeleteTokenCommandValidator : AbstractValidator<DeleteTokenCommand>
    {
        public DeleteTokenCommandValidator()
        {
            this.RuleFor(x => x.OAuthTokenId).NotEmpty();
        }
    }
}