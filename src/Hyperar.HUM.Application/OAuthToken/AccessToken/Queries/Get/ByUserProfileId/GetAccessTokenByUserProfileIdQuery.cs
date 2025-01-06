namespace Hyperar.HUM.Application.OAuthToken.AccessToken.Queries.Get.ByUserProfileId
{
    using System;
    using Hyperar.HUM.Shared.Models.Authorization;
    using MediatR;

    public record GetAccessTokenByUserProfileIdQuery(Guid UserProfileId) : IRequest<UserProfileToken?>;
}