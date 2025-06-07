namespace Hyperar.HUM.Application.SeniorTeam.Queries.List.ByUserProfileId
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Hyperar.HUM.Domain.Interfaces;
    using Hyperar.HUM.Shared.Models;
    using Hyperar.HUM.Shared.Models.TeamSelection;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    internal class ListSeniorTeamsByUserProfileIdQueryHandler : IRequestHandler<ListSeniorTeamsByUserProfileIdQuery, IEnumerable<SeniorTeam>>
    {
        private readonly IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository;

        public ListSeniorTeamsByUserProfileIdQueryHandler(IHattrickRepository<Domain.SeniorTeam> seniorTeamRepository)
        {
            this.seniorTeamRepository = seniorTeamRepository;
        }

        public async Task<IEnumerable<SeniorTeam>> Handle(ListSeniorTeamsByUserProfileIdQuery request, CancellationToken cancellationToken)
        {
            return await this.seniorTeamRepository.Query(x => x.Manager.UserProfileId == request.UserProfileId)
                .OrderBy(x => x.TeamIndex)
                .Select(x => new SeniorTeam(
                    new IdName(x.HattrickId, x.Name),
                    new IdName(x.Region.Country.HattrickId, x.Region.Country.Name),
                    new IdName(x.Region.HattrickId, x.Region.Name),
                    new IdName(x.League.HattrickId, x.League.Name),
                    new IdName(x.SeriesHattrickId, x.SeriesName),
                    x.LogoBytes,
                    x.Region.Country.League.FlagBytes,
                    x.League.FlagBytes))
                .ToListAsync(cancellationToken);
        }
    }
}