namespace Hyperar.HUM.Application.UserProfile.Commands.Initialize
{
    using System;
    using MediatR;

    public record InitializeUserProfileCommand(Guid UserProfileId) : IRequest;
}