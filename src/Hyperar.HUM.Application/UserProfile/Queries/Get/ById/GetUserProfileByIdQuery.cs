namespace Hyperar.HUM.Application.UserProfile.Queries.Get.ById
{
    using System;
    using Hyperar.HUM.Shared.Models.UserProfileSelection;
    using MediatR;

    public record GetUserProfileByIdQuery(Guid Id) : IRequest<UserProfile>;
}