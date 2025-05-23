﻿namespace Hyperar.HUM.Application.UserProfile.Queries.List
{
    using System.Collections.Generic;
    using Hyperar.HUM.Shared.Models.UserProfileSelection;
    using MediatR;

    public record ListUserProfilesQuery() : IRequest<IEnumerable<UserProfile>>;
}