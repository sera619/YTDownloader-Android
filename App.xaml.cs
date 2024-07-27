namespace YTDownloaderMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            this.UserAppTheme = AppTheme.Dark;

            MainPage = new AppShell();
        }

    }
}
