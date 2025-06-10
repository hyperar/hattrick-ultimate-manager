namespace Hyperar.HUM.Application.UserProfile.Commands.UpdateSelectedTeam
{
    using System;
    using MediatR;

    public class UpdateUserProfileSelectedTeamCommand : IRequest
    {
        public long SelectedTeamHattrickId { get; set; }

        public Guid UserProfileId { get; set; }
    }
}