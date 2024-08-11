namespace YTDownloaderMAUI.Services
{
    internal static class SettingsService
    {

        private const string CheckForUpdatesKey = "CheckForUpdatesOnStart";
        private const string CheckForHomepageAnimationKey = "CheckForHomepageAnimation";
        private const string StartFromDownloadPageKey = "StartFromDownloadPage";
        private static readonly bool DefaultCheckForUpdates = true;
        private static readonly bool DefaultCheckForHompageAnimation = true;
        private static readonly bool DefaultStartFromDownloadPage = false;


        public static bool GetCheckForUpdatesOnStart()
        {
            return Preferences.Get(CheckForUpdatesKey, DefaultCheckForUpdates);
        }

        public static void SetCheckForUpdatesOnStart(bool value)
        {
            Preferences.Set(CheckForUpdatesKey, value);
        }


        public static bool GetCheckForHomepageAnimation()
        {
            return Preferences.Get(CheckForHomepageAnimationKey, DefaultCheckForHompageAnimation);
        }

        public static void SetCheckForHomepageAnimation(bool value)
        {
            Preferences.Set(CheckForHomepageAnimationKey, value);
        }


        public static bool GetStartFromDownloadPage()
        {
            return Preferences.Get(StartFromDownloadPageKey, DefaultStartFromDownloadPage);
        }

        public static void SetStartFromDownloadPage(bool value) 
        {
            Preferences.Set(StartFromDownloadPageKey, value);
        }

    }
}
