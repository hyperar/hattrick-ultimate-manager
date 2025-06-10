namespace Hyperar.HUM.UserInterface.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Hyperar.HUM.Application.ChppFile.Download.Command;
    using Hyperar.HUM.Shared.Models.Download;
    using Hyperar.HUM.UserInterface.State.Interfaces;
    using Hyperar.HUM.UserInterface.Store.Interfaces;
    using MediatR;

    internal partial class DownloadViewModel : ViewModelBase
    {
        private CancellationTokenSource? cancellationTokenSource;

        [ObservableProperty]
        private DownloadReport downloadReport;

        public DownloadViewModel(
            INavigator navigator,
            ISessionStore sessionStore,
            ISender sender) : base(navigator, sessionStore, sender)
        {
            this.DownloadReport = new DownloadReport(
                new List<DownloadTask>(),
                null,
                0,
                0,
                false);

            this.ExecuteDownloadCommand = new AsyncRelayCommand(this.ExecuteDownloadAsync);
            this.CancelDownloadCommand = new AsyncRelayCommand(this.CancelDownloadAsync);
        }

        public AsyncRelayCommand CancelDownloadCommand { get; }

        public AsyncRelayCommand ExecuteDownloadCommand { get; }

        public override async Task InitializeAsync()
        {
            this.Navigator.ResumeNavigation();

            await base.InitializeAsync();
        }

        private async Task CancelDownloadAsync()
        {
            ArgumentNullException.ThrowIfNull(this.cancellationTokenSource);

            await this.cancellationTokenSource.CancelAsync();
        }

        private async Task ExecuteDownloadAsync()
        {
            ArgumentNullException.ThrowIfNull(this.SessionStore.SelectedUserProfileId);

            this.Navigator.SuspendNavigation();

            this.cancellationTokenSource = new CancellationTokenSource();

            var progressReport = new Progress<DownloadReport>(report =>
            {
                this.DownloadReport = report;
            });

            await this.Sender.Send(
                new DownloadCommand(
                    this.SessionStore.SelectedUserProfileId.Value,
                    progressReport),
                    this.cancellationTokenSource.Token);

            this.cancellationTokenSource = null;

            this.Navigator.ResumeNavigation();
        }
    }
}