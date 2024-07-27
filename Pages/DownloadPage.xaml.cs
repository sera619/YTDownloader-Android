using CommunityToolkit.Maui.Core.Platform;
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
            await SingleURLEntry.HideKeyboardAsync(CancellationToken.None);
            string? clipboardText = await Utils.ReadFromClipboard();
            if (string.IsNullOrEmpty(clipboardText))
            {
                await DisplayAlert("No content in clipboard", "No content found on your clipboard storage!", "OK");
                return;
            }
            SingleURLEntry.Text = clipboardText;
        }



        private async void HelpButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//HelpPage");
        }

        private async void AddVideoButton_Clicked(object sender, EventArgs e)
        {
            await SingleURLEntry.HideKeyboardAsync(CancellationToken.None);
        }
    }
}
