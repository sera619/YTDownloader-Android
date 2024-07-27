using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace YTDownloaderMAUI.Services
{
    internal static class VersionService
    {
        private const string GitHubApiUrl = "https://api.github.com/repos/sera619/YTDownloader-Android/releases/latest";

        public static async Task<string> GetLastestVersionAsync()
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
            var response = await httpClient.GetFromJsonAsync<GithubRelease>(GitHubApiUrl);
            return response?.TagName;
        }

        public static async Task<bool> CheckVersionAsync()
        {
            var currentVersion = VersionTracking.CurrentVersion;
            var lastestVersion = await GetLastestVersionAsync();
            return IsNewerVersion(lastestVersion, currentVersion);
        }

        private static bool IsNewerVersion(string latestVersion, string currentVersion)
        {
            if (string.IsNullOrWhiteSpace(latestVersion) || string.IsNullOrEmpty(currentVersion))
            {
                return false;
            }

            var latest = Version.Parse(latestVersion.TrimStart('v'));
            var current = Version.Parse(currentVersion);
            return latest > current;
        }
    }

    internal class GithubRelease
    {
        [JsonPropertyName("tag_name")]
        public required string TagName { get; set; }
    }
}
