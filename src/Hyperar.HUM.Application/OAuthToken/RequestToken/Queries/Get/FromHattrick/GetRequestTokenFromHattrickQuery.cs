namespace Hyperar.HUM.Application.OAuthToken.RequestToken.Queries.Get.FromHattrick
{
    using Hyperar.HUM.Shared.Models.Authorization;
    using MediatR;

    public record GetRequestTokenFromHattrickQuery() : IRequest<RequestToken>;
}