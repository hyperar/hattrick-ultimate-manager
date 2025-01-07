namespace Hyperar.HUM.Application.UserProfile.Commands.Initialize
{
    using MediatR;

    public record InitializeUserProfileCommand(Guid UserProfileId) : IRequest;
}