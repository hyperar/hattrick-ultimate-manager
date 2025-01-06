namespace Hyperar.HUM.Application.ChppFile.Download.Command
{
    using System;
    using Hyperar.HUM.Shared.Models.Download;
    using MediatR;

    public record DownloadCommand(Guid UserProfileId, IProgress<DownloadReport> Progress) : IRequest<bool>;
}