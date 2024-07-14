
using Android.Widget;
using CommunityToolkit.Maui.Views;
using YTDownloaderMAUI.Services;
using YTDownloaderMAUI.Src;
using YTDownloaderMAUI.Views;

namespace YTDownloaderMAUI.Pages;

public partial class HomePage : ContentPage
{

    private readonly LocalNotificationService _notificationService;
    public HomePage()
	{
		InitializeComponent();
        _notificationService = new LocalNotificationService();

#if ANDROID
            //RequestStoragePermission();
          
#endif
    }

#if ANDROID

    private async Task ShowNotification()
    {
        await _notificationService.ShowNotificationAsync("Test Notification", "This is a test notification from yt downloader.", DateTime.Now.AddSeconds(1));
    }

    private async void NotifyButton_Clicked(object sender, EventArgs e)
    {
        var popup = new YTPopup("Popup Title", "This is a message", "Confirm", "Decline");
        var result = await this.ShowPopupAsync(popup);
        if (result is bool boolResult)
        {
            if (boolResult)
            {
            }
        }
    }

#endif
}
