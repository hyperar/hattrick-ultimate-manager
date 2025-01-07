namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Queries.Get.FromHattrick
{
    using Hyperar.HUM.Shared.Models.Authorization;
    using MediatR;

    public record GetAccessTokenFromHattrickQuery(string VerificationCode, RequestToken RequestToken) : IRequest<AccessToken>;
}