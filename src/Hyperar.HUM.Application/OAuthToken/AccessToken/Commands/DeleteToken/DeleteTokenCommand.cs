namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.DeleteToken
{
    using System;
    using MediatR;

    public record DeleteTokenCommand(Guid OAuthTokenId) : IRequest;
}