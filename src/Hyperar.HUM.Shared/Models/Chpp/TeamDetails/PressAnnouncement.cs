namespace Hyperar.HUM.Shared.Models.Chpp.TeamDetails
{
    using System;

    public record PressAnnouncement(DateTime SendDate, string? Subject, string? Body);
}