namespace Hyperar.HUM.Shared.Models.Authorization
{
    using System;
    using Hyperar.HUM.Shared.Enums;

    public record UserProfileToken(Guid Id, string Token, string Secret, ChppScope Scope, DateTime CreatedOn, DateTime ExpiresOn);
}