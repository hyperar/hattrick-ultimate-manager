namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.SaveToken
{
    using FluentValidation;

    internal class SaveTokenCommandValidator : AbstractValidator<SaveTokenCommand>
    {
        public SaveTokenCommandValidator()
        {
            this.RuleFor(x => x.UserProfileId).NotEmpty();
            this.RuleFor(x => x.AccessToken.Token).NotNull().NotEmpty();
            this.RuleFor(x => x.AccessToken.Secret).NotNull().NotEmpty();
        }
    }
}