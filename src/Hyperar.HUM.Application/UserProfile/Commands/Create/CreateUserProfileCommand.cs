namespace Hyperar.HUM.Application.UserProfile.Commands.Create
{
    using MediatR;

    public record CreateUserProfileCommand() : IRequest<Domain.UserProfile>;
}