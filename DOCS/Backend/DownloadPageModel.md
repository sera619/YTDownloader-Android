# DownloadSingleViewModel Documentation

## Overview
`DownloadSingleViewModel` is a ViewModel class designed for a .NET MAUI application that allows users to download audio from YouTube videos. It manages the process of adding YouTube videos or playlists to a download queue, and handles the downloading of audio files to the device's storage.

## Key Properties

- `VideoEntries`: An `ObservableCollection<VideoEntry>` that holds the list of videos to be downloaded.
- `IsBusy`: A boolean indicating whether the ViewModel is currently processing a task.
- `IsPlaylistDownload`: A boolean toggle between single video and playlist download modes.
- `StatusMessage`: A string property used to update the UI with the current status of operations.

## Main Functions

1. **Adding Videos**: 
   - Single videos can be added using the `AddSingleVideoAsync` method.
   - Playlists can be added using the `AddPlaylistVideosAsync` method.

2. **Downloading**:
   - The `DownloadAllEntriesAsync` method processes all videos in the `VideoEntries` collection.
   - For Android 10 (API level 29) and above, it uses the `SaveFileScopedStorage` method to comply with scoped storage requirements.

3. **User Interface Updates**:
   - The class uses data binding to update the UI, with properties like `VideoModeText` and `UrlPlaceholderText` changing based on the `IsPlaylistDownload` state.

## Key Methods

### `AddVideoAsync(string videoUrl)`
Determines whether to add a single video or a playlist based on the `IsPlaylistDownload` property.

### `AddSingleVideoAsync(string videoUrl)`
Adds a single video to the `VideoEntries` collection after validating the URL and fetching video details.

### `AddPlaylistVideosAsync(string videoUrl)`
Adds all videos from a YouTube playlist to the `VideoEntries` collection.

### `DownloadAllEntriesAsync()`
Iterates through all entries in `VideoEntries`, downloading each video's audio.

### `SaveFileScopedStorage(IStreamInfo streamInfo, string fileName)`
Saves the audio file using Android's scoped storage system for devices running Android 10 and above.

## Error Handling
The class includes error handling for invalid URLs, missing videos, and issues during the download process. Error messages are displayed to the user via alert dialogs.

## Notes for Developers
- Ensure that the necessary permissions are set in the Android manifest file for file access.
- For Android 10 and above, the app uses scoped storage. Older versions use the traditional file system access.
- The class uses the YoutubeExplode library for interacting with YouTube and downloading video streams.
- UI updates are performed on the main thread using `MainThread.BeginInvokeOnMainThread()`.

## Future Improvements
- Implement progress reporting for individual file downloads.
- Add support for selecting video quality or format before download.
- Implement a feature to resume interrupted downloads.