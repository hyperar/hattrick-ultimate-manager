namespace Hyperar.HUM.Shared.Models.UserProfileSelection
{
    using System;

    public record UserProfile(
        Guid Id,
        bool HasAuthorized,
        DateTime? LastDownloadDate,
        long? SelectedTeamHattrickId,
        long? HattrickId,
        string? UserName,
        byte[]? AvatarBytes,
        byte[]? CountryFlagBytes);
}