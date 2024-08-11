using YTDownloaderMAUI.Services;

namespace YTDownloaderMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            this.UserAppTheme = AppTheme.Dark;
            if (SettingsService.GetStartFromDownloadPage())
            {
                MainPage = new AppShell();
                Shell.Current.GoToAsync("//DownloadPage").ConfigureAwait(false);
            }
            else
            {
                MainPage = new AppShell();

            }
        }

    }
}
