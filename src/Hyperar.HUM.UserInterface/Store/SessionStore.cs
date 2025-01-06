namespace Hyperar.HUM.UserInterface.Store
{
    using System;
    using System.Threading.Tasks;
    using Hyperar.HUM.UserInterface.Store.Interfaces;

    internal class SessionStore : ISessionStore
    {
        private long? selectedTeamHattrickId;

        private Guid? selectedUserProfileId;

        public event Func<Task>? SelectedTeamChanged;

        public event Func<Task>? SelectedUserProfileChanged;

        public long? SelectedTeamHattrickId
        {
            get
            {
                return this.selectedTeamHattrickId;
            }

            private set
            {
                this.selectedTeamHattrickId = value;

                this.SelectedTeamChanged?.Invoke();
            }
        }

        public Guid? SelectedUserProfileId
        {
            get
            {
                return this.selectedUserProfileId;
            }

            private set
            {
                this.selectedUserProfileId = value;

                this.SelectedUserProfileChanged?.Invoke();
            }
        }

        public void SetSelectedTeam(long teamHattrickId)
        {
            this.SelectedTeamHattrickId = teamHattrickId;
        }

        public void SetSelectedUserProfile(Guid userProfileId)
        {
            this.SelectedUserProfileId = userProfileId;
        }
    }
}