namespace Hyperar.HUM.UserInterface.Store.Interfaces
{
    using System;
    using System.Threading.Tasks;

    internal interface ISessionStore
    {
        event Func<Task>? SelectedTeamChanged;

        event Func<Task>? SelectedUserProfileChanged;

        long? SelectedTeamHattrickId { get; }

        Guid? SelectedUserProfileId { get; }

        void SetSelectedTeam(long teamHattrickId);

        void SetSelectedUserProfile(Guid userProfileId);
    }
}