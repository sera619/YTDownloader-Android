using Android.Content;
using Android.OS;
using Android.Provider;
using System.Collections.ObjectModel;
using System.Windows.Input;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using YTDownloaderMAUI.Models;
using YTDownloaderMAUI.Src;

namespace YTDownloaderMAUI.ViewModels
{
    public class DownloadViewModel : BindableObject
    {
        public ObservableCollection<VideoEntry> VideoEntries { get; set; }
        public ICommand DeleteCommand { get; }
        public ICommand AddDummyCommand { get; }
        public ICommand DownloadAllEntriesCommand { get; }
        public ICommand ClearSingleCommand { get; }
        public ICommand AddFileCommand { get; }
        public ICommand CancelDownloadCommand { get; }
        public ICommand OpenFileLocationCommand { get; }

        private CancellationTokenSource? _cancellationTokenSource;

        private int _videoAmount;
        public int VideoAmount
        {
            get => _videoAmount;
            set
            {
                _videoAmount = value;
                OnPropertyChanged();
            }
        }

        private string _videoAmountText = "Download Queue";
        public string VideoAmountText
        {
            get => _videoAmountText;
            set
            {
                _videoAmountText = value;
                OnPropertyChanged();
            }
        }

        private bool _canCancel = false;
        public bool CanCancel
        {
            get => _canCancel;
            set
            {
                _canCancel = value;
                OnPropertyChanged();
            }
        }

        private static readonly string[] _dummyURLS = {
            "https://www.youtube.com/watch?v=QWA-fWBQh0A",
            "https://www.youtube.com/watch?v=dQw4w9WgXcQ"
            // Additional dummy URLs
        };

        private bool _showButtons = true;
        public bool ShowButtons {get => _showButtons; set { _showButtons = value; OnPropertyChanged(); }}

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private bool _isPlaylistDownload = false;
        public bool IsPlaylistDownload
        {
            get => _isPlaylistDownload;
            set
            {
                _isPlaylistDownload = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(VideoModeText));
                OnPropertyChanged(nameof(UrlPlaceholderText));
            }
        }

        public string VideoModeText => IsPlaylistDownload ? "Playlist" : "Single video";
        public string UrlPlaceholderText => IsPlaylistDownload ? "Enter a youtube playlist url..." : "Enter a youtube video url...";

        private string _singleUrlEntryText = string.Empty;
        public string SingleUrlEntryText
        {
            get => _singleUrlEntryText;
            set
            {
                _singleUrlEntryText = value;
                OnPropertyChanged();
            }
        }

        private string _defaultStatusText = "Insert a URL to add video to the download queue.";

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public DownloadViewModel()
        {
            VideoEntries = new ObservableCollection<VideoEntry>();
            DeleteCommand = new Command<VideoEntry>(DeleteEntry);
            AddDummyCommand = new Command(async () => await AddDummys());
            DownloadAllEntriesCommand = new Command(async () => await DownloadAllEntriesAsync());
            AddFileCommand = new Command<string>(async (url) => await AddVideoAsync(url));
            ClearSingleCommand = new Command(async () => await ClearSingle());
            CancelDownloadCommand = new Command(CancelDownload, () => CanCancel);
            OpenFileLocationCommand = new Command(OpenFileLocation);
            VideoEntries.CollectionChanged += (s, e) => UpdateVideoAmount();

            StatusMessage = _defaultStatusText;
        }

        private void UpdateVideoAmount()
        {
            VideoAmount = VideoEntries.Count;
            if(VideoEntries.Count > 0)
            {
                VideoAmountText = $"{VideoAmount} Video(s) in download queue";
            }
            else
            {
                VideoAmountText = "Download Queue";
            }
        }

        private void CancelDownload()
        {
            _cancellationTokenSource?.Cancel();
            CanCancel = false;
        }

        public async Task DownloadAllEntriesAsync()
        {
            if (VideoEntries.Count == 0)
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await App.Current?.MainPage?.DisplayAlert("No Entries", "There are no entries to download.", "OK");
                });
                return;
            }

            int maxCount = VideoEntries.Count;
            int currentCount = 0;

            IsBusy = true;
            CanCancel = true;
            StatusMessage = "Downloading all entries...";

            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                foreach (var entry in VideoEntries.ToList())
                {
                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        StatusMessage = "Download cancelled.";
                        break;
                    }

                    currentCount += 1;
                    StatusMessage = $"Downloading file {currentCount}/{maxCount}...";
                    ShowButtons = false;

                    await Task.Run(async () =>
                    {
                        try
                        {
                            YoutubeClient client = new YoutubeClient();
                            var video = await client.Videos.GetAsync(entry.URL);
                            string videoTitle = video.Title;
                            var streamInfoSet = await client.Videos.Streams.GetManifestAsync(entry.URL);
                            var streamInfo = streamInfoSet.GetAudioStreams().GetWithHighestBitrate();
                            if (streamInfo != null)
                            {
                                string fileName = $"{CleanFileName(videoTitle)}.mp3";
                                if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                                {
                                    await SaveFileScopedStorage(streamInfo, fileName, _cancellationTokenSource.Token);
                                }
                                else
                                {
                                    string downloadsPath = Utils.GetDownloadsPath();
                                    string fullPath = Path.Combine(downloadsPath, fileName);
                                    await client.Videos.Streams.DownloadAsync(streamInfo, fullPath, null, _cancellationTokenSource.Token);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Inner exception: {ex}");
                            throw;
                        }
                    }, _cancellationTokenSource.Token);

                    await MainThread.InvokeOnMainThreadAsync(() => VideoEntries.Remove(entry));
                }

                if (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        StatusMessage = _defaultStatusText;
                        await App.Current?.MainPage?.DisplayAlert("Download finished", "All files are successfully downloaded.", "OK");
                    });
                }
            }
            catch (System.OperationCanceledException)
            {
                StatusMessage = "Download cancelled.";
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    StatusMessage = $"Error during download: {ex.Message}";
                });
                System.Diagnostics.Debug.WriteLine($"Outer exception: {ex}");

                if (ex.InnerException != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException}");
                }
            }
            finally
            {
                IsBusy = false;
                CanCancel = false;
                ShowButtons = true;
                _cancellationTokenSource = null;
            }
        }

        private static string CleanFileName(string fileName)
        {
            return string.Join("_", fileName.Split(Path.GetInvalidFileNameChars()));
        }

        private async Task AddVideoAsync(string videoUrl)
        {
            if (string.IsNullOrEmpty(videoUrl))
                return;

            IsBusy = true;
            ShowButtons = false;
            if (!IsPlaylistDownload)
            {
                await AddSingleVideoAsync(videoUrl);
            }
            else
            {
                await AddPlaylistVideosAsync(videoUrl);
            }
        }

        private async Task AddPlaylistVideosAsync(string videoUrl)
        {
            StatusMessage = "Adding files to list...";
            if (!Utils.IsYouTubePlaylistUrl(videoUrl))
            {
                IsBusy = false;
                StatusMessage = _defaultStatusText;
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await App.Current?.MainPage?.DisplayAlert("Playlist Error", $"Your URL:\n\n'{videoUrl}'\n\nis not a valid YouTube playlist URL.", "OK");
                });
                ShowButtons = true;
                return;
            }

            YoutubeClient client = new YoutubeClient();
            await foreach (var video in client.Playlists.GetVideosAsync(videoUrl))
            {
                if (video == null)
                {
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await App.Current?.MainPage?.DisplayAlert("Playlist Error", $"No videos found in url:\n\n'{videoUrl}'\n\nPlease check url and try again!", "OK");
                    });
                    continue;
                }
                else
                {
                    VideoEntry newEntry = new VideoEntry
                    {
                        URL = video.Url,
                        Title = Utils.TruncateText(video.Title),
                        Duration = video.Duration?.ToString() ?? "Unknown"
                    };
                    MainThread.BeginInvokeOnMainThread(() => VideoEntries.Add(newEntry));
                }
            }
            SingleUrlEntryText = string.Empty;
            IsBusy = false;
            ShowButtons = true;

            StatusMessage = _defaultStatusText;
        }

        private async Task AddSingleVideoAsync(string videoUrl)
        {
            StatusMessage = "Adding file to list...";
            if (!Utils.IsYouTubeUrl(videoUrl))
            {
                IsBusy = false;
                StatusMessage = _defaultStatusText;
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await App.Current?.MainPage?.DisplayAlert("Video Error", $"Your URL:\n\n'{videoUrl}'\n\nis not a valid YouTube video URL.", "OK");
                });
                ShowButtons = true;
                return;
            }
            if (videoUrl.Contains("youtu.be"))
            {
                videoUrl = Utils.ConvertShortUrlToLongUrl(videoUrl);
            }

            YoutubeClient client = new YoutubeClient();
            var video = await client.Videos.GetAsync(videoUrl);
            if (video == null)
            {
                IsBusy = false;
                StatusMessage = string.Empty;
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await App.Current?.MainPage?.DisplayAlert("Video Error", $"No video found in your url:\n\n{videoUrl}\n\nPlease check url and try again!", "OK");
                });
                ShowButtons = true;
                return;
            }
            SingleUrlEntryText = string.Empty;
            VideoEntry videoEntry = new VideoEntry
            {
                URL = video.Url,
                Title = Utils.TruncateText(video.Title),
                Duration = video.Duration?.ToString() ?? "Unknown"
            };

            VideoEntries.Add(videoEntry);
            IsBusy = false;
            ShowButtons = true;

            StatusMessage = _defaultStatusText;
        }

        private async Task SaveFileScopedStorage(IStreamInfo streamInfo, string fileName, CancellationToken cancellationToken)
        {
            try
            {
                var contentResolver = Android.App.Application.Context.ContentResolver;
                var values = new ContentValues();
                values.Put(MediaStore.IMediaColumns.DisplayName, fileName);
                values.Put(MediaStore.IMediaColumns.MimeType, "audio/mpeg");
                values.Put(MediaStore.IMediaColumns.RelativePath, Android.OS.Environment.DirectoryMusic);

                var uri = contentResolver.Insert(MediaStore.Audio.Media.ExternalContentUri, values);
                if (uri != null)
                {
                    using (var outputStream = contentResolver.OpenOutputStream(uri))
                    {
                        if (outputStream != null)
                        {
                            var youtubeClient = new YoutubeClient();
                            await youtubeClient.Videos.Streams.CopyToAsync(streamInfo, outputStream, null, cancellationToken);
                        }
                    }
                }
            }
            catch (System.OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await App.Current?.MainPage?.DisplayAlert("Video Download Error", $"Something went wrong with your download.\n\nPlease contact the developer!\n\n{ex.Message}", "OK");
                });
            }
        }

        private void DeleteEntry(VideoEntry entry)
        {
            if (entry != null && VideoEntries.Contains(entry))
            {
                VideoEntries.Remove(entry);
                if (VideoEntries.Count == 0)
                {
                    StatusMessage = _defaultStatusText;
                }
            }
        }

        private async Task AddDummys()
        {
            StatusMessage = "Adding dummys to list...";
            int maxCount = _dummyURLS.Length;
            for (int i = 0; i < maxCount; i++)
            {
                IsBusy = true;
                ShowButtons = false;
                StatusMessage = $"Adding dummy {i + 1}/{maxCount}...";
                await AddSingleVideoAsync(_dummyURLS[i]);
            }
            ShowButtons = true;
        }

        private async Task ClearSingle()
        {
            if (VideoEntries.Count == 0)
            {
                return;
            }
            if (App.Current?.MainPage != null)
            {
                bool isOkay = await App.Current.MainPage.DisplayAlert("Clear video list", $"Are you sure that you want to delete all {VideoEntries.Count} videos from the list?", "Yes", "No");
                if (isOkay)
                {
                    MainThread.BeginInvokeOnMainThread(() => VideoEntries.Clear());
                    StatusMessage = _defaultStatusText;
                }
            }
        }

        private void OpenFileLocation()
        {
            var intent = new Intent(Intent.ActionView);
            var uri = Android.Net.Uri.Parse(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryRingtones).AbsolutePath);
            intent.SetDataAndType(uri, "*/*");
            intent.AddFlags(ActivityFlags.NewTask);
            try
            {
                Android.App.Application.Context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to open file location: {ex}");
            }
        }

    }
}