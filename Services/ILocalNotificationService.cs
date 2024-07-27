namespace YTDownloaderMAUI.Services
{
    internal interface ILocalNotificationService
    {
        Task ShowNotificationAsync(string title, string message, DateTime? notifyTime = null);
    }
}
