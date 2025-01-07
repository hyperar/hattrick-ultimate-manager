namespace Hyperar.HUM.Application.OAuthToken.AuthorizationUrl.Queries.Get.ForRequestToken
{
    using Hyperar.HUM.Shared.Models.Authorization;
    using MediatR;

    public record GetAuthorizationUrlForRequestTokenQuery(RequestToken RequestToken) : IRequest<string>;
}