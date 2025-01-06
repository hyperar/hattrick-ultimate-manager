namespace Hyperar.HUM.UserInterface.Views
{
    using System;
    using Avalonia.Controls;
    using Hyperar.HUM.Shared.Models.Download;

    public partial class DownloadView : UserControl
    {
        public DownloadView()
        {
            this.InitializeComponent();
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);
        }

        private void DataGrid_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            if (this.DataGridTasks.SelectedItem is DownloadTask downloadTask)
            {
                this.DataGridTasks.ScrollIntoView(
                    downloadTask,
                    this.DataGridTasks.Columns[0]);
            }
        }
    }
}