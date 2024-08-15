using CommunityToolkit.Maui.Core.Platform;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using YTDownloaderMAUI.Models;
using YTDownloaderMAUI.Src;

namespace YTDownloaderMAUI.Pages;

public partial class NewDownloadPage : ContentPage
{

    public NewDownloadPage()
	{
		InitializeComponent();
    }

    private async void PasteUrlButton_Clicked(object sender, EventArgs e)
    {
        await UrlEntry.HideKeyboardAsync(CancellationToken.None);
        UrlEntry.Unfocus();
        string? clipboardText = await Utils.ReadFromClipboard();
        if (string.IsNullOrEmpty(clipboardText))
        {
            await DisplayAlert("No content in clipboard", "No content found on your clipboard storage!", "OK");
            return;
        }
        UrlEntry.Text = clipboardText;
    }


    private async void AddVideoToListButton_Clicked(object sender, EventArgs e)
    {
        await UrlEntry.HideKeyboardAsync(CancellationToken.None);
        UrlEntry.Unfocus();
    }
}