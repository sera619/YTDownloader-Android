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
        CheckVersions();

        HomepageIcon.HeightRequest = Utils.GetDeviceWidth() / 2;

#if ANDROID
        //RequestStoragePermission();

#endif
    }

#if ANDROID

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (SettingsService.GetCheckForHomepageAnimation())
        {
            StartIntroAnimation();
        }
    }

    private async void StartIntroAnimation()
    {
        HomepageInfoLayout.Opacity = 0;
        HomepageIcon.Rotation = 0;
        HomepageIcon.Scale = 0;
        await Task.WhenAny<bool>
        (
          HomepageIcon.RotateTo(360, 2150),
          HomepageIcon.ScaleTo(1.3, 1250)
        );
        await HomepageIcon.ScaleTo(1, 1000);
        await HomepageInfoLayout.FadeTo(1, 1750);
        HomepageIcon.Rotation = 0;
    }


    private async void CheckVersions()
    {
        if (SettingsService.GetCheckForUpdatesOnStart() == true)
        {
            bool isUpdateAvailable = await VersionService.CheckVersionAsync();
            if (isUpdateAvailable)
            {
                ShowUpdatePopup();
                await ShowNotification();
            }
        }
    }

    private async Task ShowNotification()
    {
        await _notificationService.ShowNotificationAsync("YT Downloader Update", "A new version of YT Downloader Android ist available.", DateTime.Now.AddSeconds(1));
    }


    private async void ShowUpdatePopup()
    {
        string? newVersionName = await VersionService.GetLastestVersionAsync();
        var popup = new YTPopup("New Update!", $"A new update '{newVersionName}' is availble!\n\nYour current version is 'v{VersionTracking.CurrentVersion}'!\n\nDo you want to download it?", "Yes", "No");
        var result = await this.ShowPopupAsync(popup);
        if (result is bool boolResult)
        {
            if (boolResult)
            {
                Uri uri = new Uri("https://github.com/sera619/YTDownloader-Android/releases");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
        }

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
