namespace Hyperar.HUM.Application.UserProfile.Queries.Get.ById
{
    using FluentValidation;

    internal class GetUserProfileByIdValidator : AbstractValidator<GetUserProfileByIdQuery>
    {
        public GetUserProfileByIdValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();
        }
    }
}