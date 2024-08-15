using System.Windows.Input;

namespace YTDownloaderMAUI.ViewModels
{
    public class AboutViewModel : BindableObject
    {
        public ICommand OpenUrlInBrowserCommand { get; }

        private string _versionText = string.Empty;
        public string VersionText
        {
            get => _versionText;
            set
            {
                _versionText = value;
            }
        }

        public AboutViewModel()
        {
            VersionText = VersionTracking.CurrentVersion;
            OpenUrlInBrowserCommand = new Command<string>(async (urlstring) => await OpenUrlInBrowser(urlstring));
        }

        private static async Task OpenUrlInBrowser(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error open url in browser: {ex.Message}");
            }
        }
    }
}
