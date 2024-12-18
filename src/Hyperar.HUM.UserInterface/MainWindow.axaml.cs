namespace Hyperar.HUM.UserInterface
{
    using FluentAvalonia.UI.Windowing;
    using Microsoft.Extensions.Configuration;

    public partial class MainWindow : AppWindow
    {
        private readonly IConfiguration? configuration;

        public MainWindow()
        {
            this.InitializeComponent();

            this.TitleBar.ExtendsContentIntoTitleBar = true;
            this.TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;
        }

        public MainWindow(IConfiguration configuration) : this()
        {
            this.configuration = configuration;

            //Bitmap bitmap = new Bitmap(
            //    AssetLoader.Open(
            //        new Uri(
            //            "avares://Hyperar.HUM.UserInterface/Assets/Icons/hum.ico")));

            //if (bitmap is IImage icon)
            //{
            //    this.Icon = icon;
            //}

            this.Title = this.configuration["app:Title"];
        }
    }
}