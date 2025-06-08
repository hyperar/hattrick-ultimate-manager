namespace Hyperar.HUM.Shared.Models.UserProfileSelection
{
    using System;

    public record UserProfile(
        Guid Id,
        bool HasAuthorized,
        DateTime? LastDownloadDate,
        long? SelectedTeamHattrickId,
        IdName? Manager,
        IdName? Country,
        byte[]? AvatarBytes,
        byte[]? CountryFlagBytes);
}