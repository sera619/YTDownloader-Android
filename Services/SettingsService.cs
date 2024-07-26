using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTDownloaderMAUI.Services
{
    internal static class SettingsService
    {

        private const string CheckForUpdatesKey = "CheckForUpdatesOnStart";
        private static readonly bool DefaultCheckForUpdates = true;

        public static bool GetCheckForUpdatesOnStart()
        {
            return Preferences.Get(CheckForUpdatesKey, DefaultCheckForUpdates);
        }

        public static void SetCheckForUpdatesOnStart(bool value) 
        {
            Preferences.Set(CheckForUpdatesKey, value);
        }

    }
}
