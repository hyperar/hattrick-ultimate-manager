namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.RevokeToken
{
    using System;
    using MediatR;

    public record RevokeTokenCommand(string Token, string Secret, DateTime CreatedOn, DateTime ExpiresOn) : IRequest;
}