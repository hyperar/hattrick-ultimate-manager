namespace Hyperar.HUM.Shared.Models.Authorization
{
    using System;

    public record AccessToken(string Token, string Secret, DateTime CreatedOn, DateTime ExpiresOn);
}