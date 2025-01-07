namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.SaveToken
{
    using System;
    using Hyperar.HUM.Shared.Models.Authorization;
    using MediatR;

    public record SaveTokenCommand(Guid UserProfileId, AccessToken AccessToken) : IRequest;
}