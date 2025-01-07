namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Commands.CheckToken
{
    using Hyperar.HUM.Shared.Models.Authorization;
    using MediatR;

    public record CheckAccessTokenCommand(UserProfileToken UserProfileToken) : IRequest<bool>;
}