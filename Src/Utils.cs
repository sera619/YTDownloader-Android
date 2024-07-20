using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

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
                @"^https:\/\/youtube\.com\/watch\?v=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$",
                @"^https:\/\/www\.youtube\.com\/watch\?v=[a-zA-Z0-9_-]+(&[a-zA-Z0-9_-]+=[a-zA-Z0-9_%]+)*$", // Desktop YouTube Video
                @"^https:\/\/youtu\.be\/[a-zA-Z0-9_-]+$", // Shortened YouTube Video
                @"^https:\/\/www\.youtube\.com\/embed\/[a-zA-Z0-9_-]+$", // Embedded YouTube Video
                @"^https:\/\/www\.youtube\.com\/v\/[a-zA-Z0-9_-]+$", // Old-style Embedded YouTube Video
                @"^https:\/\/www\.youtube\.com\/user\/[a-zA-Z0-9_-]+#p\/u\/[0-9]+\/[a-zA-Z0-9_-]+$", // User Upload
                @"^https:\/\/m\.youtube\.com\/user\/[a-zA-Z0-9_-]+#p\/u\/[0-9]+\/[a-zA-Z0-9_-]+$",// Mobile User Upload
                @"(?:youtu\.be/|youtube\.com/(?:watch\?v=|embed/|v/|.*[?&]v=))([a-zA-Z0-9_-]{11})"
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

        public static string ConvertShortUrlToLongUrl(string url)
        {
            string videoId = ExtractVideoId(url);
            return videoId != null ? $"https://www.youtube.com/watch?v={videoId}" : null;
        }

        private static string ExtractVideoId(string url)
        {
            string pattern = @"(?:youtu\.be/|youtube\.com/(?:watch\?v=|embed/|v/|.*[?&]v=))([a-zA-Z0-9_-]{11})";
            Match match = Regex.Match(url, pattern);
            return match.Success ? match.Groups[1].Value : null;
        }

        public static async Task<string> ReadFromClipboard()
        {
            if (Clipboard.HasText)
            {
                string? clipboardText = await Clipboard.GetTextAsync();
                if (string.IsNullOrEmpty(clipboardText))
                {
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
