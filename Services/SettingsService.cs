namespace YTDownloaderMAUI.Services
{
    internal static class SettingsService
    {

        private const string CheckForUpdatesKey = "CheckForUpdatesOnStart";
        private const string CheckForHomepageAnimationKey = "CheckForHomepageAnimation";
        private static readonly bool DefaultCheckForUpdates = true;
        private static readonly bool DefaultCheckForHompageAnimation = true;

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

    }
}
