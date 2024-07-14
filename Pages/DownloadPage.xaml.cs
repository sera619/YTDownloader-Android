using YTDownloaderMAUI.Src;

namespace YTDownloaderMAUI.Pages
{
    public partial class DownloadPage : ContentPage
    {
        public DownloadPage()
        {
            InitializeComponent();
        }

        private async void PasteClipboardButton_Clicked(object sender, EventArgs e)
        {
            string? clipboardText = await Utils.ReadFromClipboard();
            if (string.IsNullOrEmpty(clipboardText)) 
            {
                await DisplayAlert("No content in clipboard", "No content found on your clipboard storage!", "OK");
                return;                 
            }
            SingleURLEntry.Text = clipboardText;
        }
    }
}
