namespace Hyperar.HUM.Application.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediatR;

    public class UserProfileController
    {
        private readonly ISender sender;

        public UserProfileController(ISender sender)
        {
            this.sender = sender;
        }

        public async Task<ICollection<Domain.UserProfile>> ListAsync()
        {
            return await this.sender.Send(
                new Features.UserProfile.ListRequest());
        }
    }
}