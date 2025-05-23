namespace Hyperar.HUM.Application.SeniorTeam.Queries.List.ByUserProfileId
{
    using System;
    using System.Collections.Generic;
    using Hyperar.HUM.Shared.Models.TeamSelection;
    using MediatR;

    public record ListSeniorTeamsByUserProfileIdQuery(Guid UserProfileId) : IRequest<IEnumerable<SeniorTeam>>;
}
