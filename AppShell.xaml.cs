using CommunityToolkit.Maui.Views;
using YTDownloaderMAUI.Pages;
using YTDownloaderMAUI.Views;

namespace YTDownloaderMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void AppMenuExitButton_Clicked(object sender, EventArgs e)
        {
            var popup = new YTPopup("Exit YT Downloader", "Do you really want to exit?", "Confirm", "Decline");
            var result = await this.ShowPopupAsync(popup);
            if(result is bool boolResult)
            {
                if (boolResult)
                {
#if ANDROID
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#endif
                }
                else
                {
                    return;
                }
            }
        }
    }
}
