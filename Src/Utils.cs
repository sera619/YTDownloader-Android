using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace YTDownloaderMAUI.Src
{
    internal class Utils
    {
        public static string TruncateText(string text, int maxLength = 30)
        {
            if (text.Length > maxLength)
            {
                return text.Substring(0, maxLength) + "..."; 
            }
            return text;
        }

        public static bool IsYouTubePlaylistUrl(string url)
        {
            string[] patterns = new string[]
            {
                @"^https:\/\/www\.youtube\.com\/watch\?v=[a-zA-Z0-9_-]+&list=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$",
                @"^https:\/\/m\.youtube\.com\/watch\?v=[a-zA-Z0-9_-]+&list=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$",
                @"^https:\/\/www\.youtube\.com\/playlist\?list=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$",
                @"^https:\/\/youtube\.com\/playlist\?list=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$",
                @"^https:\/\/m\.youtube\.com\/playlist\?list=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$",
                @"^https:\/\/www\.youtube\.com\/watch\?v=[a-zA-Z0-9_-]+&list=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$",
                @"^https:\/\/youtube\.com\/watch\?v=[a-zA-Z0-9_-]+&list=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$",
                @"^https:\/\/m\.youtube\.com\/watch\?v=[a-zA-Z0-9_-]+&list=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$",
                @"^https:\/\/www\.youtube\.com\/embed\/videoseries\?list=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$",
                @"^https:\/\/youtube\.com\/embed\/videoseries\?list=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$",
            };

            foreach (var pattern in patterns)
            {
                if (Regex.IsMatch(url, pattern))
                {
                    return true;
                }
            }
            return false;
        }


        public static bool IsYouTubeUrl(string url)
        {
            string[] patterns = new string[]
            {
                @"^https:\/\/m\.youtube\.com\/watch\?v=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$", // Mobile YouTube Video
                @"^https:\/\/www\.youtube\.com\/watch\?v=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$", // Desktop YouTube Video
                @"^https:\/\/youtu\.be\/[a-zA-Z0-9_-]+$", // Shortened YouTube Video
                @"^https:\/\/www\.youtube\.com\/embed\/[a-zA-Z0-9_-]+$", // Embedded YouTube Video
                @"^https:\/\/www\.youtube\.com\/v\/[a-zA-Z0-9_-]+$", // Old-style Embedded YouTube Video
                @"^https:\/\/www\.youtube\.com\/user\/[a-zA-Z0-9_-]+#p\/u\/[0-9]+\/[a-zA-Z0-9_-]+$", // User Upload
                @"^https:\/\/m\.youtube\.com\/user\/[a-zA-Z0-9_-]+#p\/u\/[0-9]+\/[a-zA-Z0-9_-]+$" // Mobile User Upload
            };
            foreach (var pattern in patterns)
            {
                if (Regex.IsMatch(url, pattern))
                {
                    return true;
                }
            }
            return false;
        }
        public static async Task<string> ReadFromClipboard()
        {
            if (Clipboard.HasText)
            {
                string? clipboardText = await Clipboard.GetTextAsync();
                if (string.IsNullOrEmpty(clipboardText)) {
                    return string.Empty;
                }
                return clipboardText;
            }
            return string.Empty;
        }
        public static string GetDownloadsPath()
        {
            string downloadsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic);
#if ANDROID
            //string? savePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads)?.AbsolutePath;
            string? savePath = Android.OS.Environment.DirectoryDownloads;
            if (!string.IsNullOrEmpty(savePath))
            {
                downloadsPath = savePath;
            }
#endif
            return downloadsPath;
        }

    }
}
