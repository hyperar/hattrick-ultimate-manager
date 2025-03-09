namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public record User(
        long UserId,
        IdName Language,
        string SupporterTier,
        string LoginName,
        string Name,
        string ICQ,
        DateTime SignupDate,
        DateTime ActivationDate,
        DateTime LastLoginDate,
        bool HasManagerLicense,
        NationalTeam[] NationalTeams);
}