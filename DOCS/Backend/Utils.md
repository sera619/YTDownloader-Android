# Additional Classes Documentation

## Utils Class

The `Utils` class in the `YTDownloaderMAUI.Src` namespace provides utility methods for the YouTube Downloader application.

### Key Methods:

1. `TruncateText(string text, int maxLength = 35)`: 
   - Truncates a given text to a specified maximum length, adding "..." if truncated.

2. `IsYouTubePlaylistUrl(string url)`: 
   - Validates if a given URL is a valid YouTube playlist URL using regex patterns.

3. `IsYouTubeUrl(string url)`: 
   - Validates if a given URL is a valid YouTube video URL using regex patterns.

4. `ReadFromClipboard()`: 
   - Asynchronously reads text from the clipboard.

5. `GetDownloadsPath()`: 
   - Returns the path to the Downloads folder, with special handling for Android.

## PermissionHelper Class

The `PermissionHelper` class manages runtime permissions for Android devices.

### Key Methods:

1. `GetRequiredPermissions()`: 
   - Returns a list of required permissions based on the Android SDK version.

2. `CheckAndRequestPermissionsAsync()`: 
   - Checks and requests necessary permissions asynchronously.

### Inner Class: PermissionReceiver

- Handles the result of permission requests.

## MainActivity Class

The `MainActivity` class is the main entry point for the Android app.

### Key Methods:

1. `OnCreate(Bundle savedInstanceState)`: 
   - Initializes the app and requests storage permissions.

2. `RequestStoragePermission()`: 
   - Requests storage permissions for Android M and above.

3. `OnRequestPermissionsResult(...)`: 
   - Handles the result of permission requests.

Note: The MainActivity class uses attributes for configuration and targets specific Android versions.