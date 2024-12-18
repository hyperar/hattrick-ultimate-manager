namespace Hyperar.HUM.Infrastructure.Features.UserProfile.List
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Hyperar.HUM.Application.Features.UserProfile;
    using Hyperar.HUM.Domain.Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    internal class Handler : IRequestHandler<ListRequest, ICollection<Domain.UserProfile>>
    {
        private readonly IRepository<Domain.UserProfile> userProfileRepository;

        public Handler(IRepository<Domain.UserProfile> userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        public async Task<ICollection<Domain.UserProfile>> Handle(ListRequest request, CancellationToken cancellationToken)
        {
            return await this.userProfileRepository.Query()
                .ToListAsync(cancellationToken);
        }
    }
}