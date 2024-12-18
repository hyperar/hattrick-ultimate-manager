namespace Hyperar.HUM.Application.Features.UserProfile
{
    using System.Collections.Generic;
    using MediatR;

    public class ListRequest : IRequest<ICollection<Domain.UserProfile>>
    {
    }
}