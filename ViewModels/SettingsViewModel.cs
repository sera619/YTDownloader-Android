using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using YTDownloaderMAUI.Services;

namespace YTDownloaderMAUI.ViewModels
{
    internal class SettingsViewModel : INotifyPropertyChanged
    {
        public ICommand CheckForUpdatesCommand { get; }
        public ICommand OpenDownloadUrlCommand { get; }
        public ICommand UpdateStorageInfoCommand { get; }

        private bool _isHomepageAnimation;
        public bool IsHomepageAnimation
        {
            get => _isHomepageAnimation;
            set
            {
                _isHomepageAnimation = value;
                OnPropertyChanged();
                SettingsService.SetCheckForHomepageAnimation(value);
            }
        }

        private bool _isCheckForUpdates;
        public bool IsCheckForUpdates
        {
            get => _isCheckForUpdates;
            set
            {
                if (_isCheckForUpdates != value)
                {
                    _isCheckForUpdates = value;
                    OnPropertyChanged();
                    SettingsService.SetCheckForUpdatesOnStart(value);
                }
            }
        }
        private string _externalStorageInfo = string.Empty;
        public string ExternalStorageInfo
        {
            get => _externalStorageInfo;
            set { _externalStorageInfo = value; OnPropertyChanged(); }
        }

        private string _internalStorageInfo = string.Empty;
        public string InternalStorageInfo
        {
            get => _internalStorageInfo;
            set { _internalStorageInfo = value; OnPropertyChanged(); }
        }

        private double _externalStorageProgress = 0;
        public double ExternalStorageProgress
        {
            get => _externalStorageProgress;
            set { _externalStorageProgress = value; OnPropertyChanged(); }
        }

        private double _internalStorageProgress = 0;
        public double InternalStorageProgress
        {
            get => _internalStorageProgress;
            set { _internalStorageProgress = value; OnPropertyChanged(); }
        }

        private const string _defaultUpdateCheckText = "Press the button to check...";

        private string _checkUpdateText = string.Empty;
        public string CheckUpdateText
        {
            get => _checkUpdateText;
            set{ _checkUpdateText = value; OnPropertyChanged(); }
        }

        private bool _isDownloadButtonVisible = false;
        public bool IsDownloadButtonVisible
        {
            get => _isDownloadButtonVisible;
            set { _isDownloadButtonVisible = value; OnPropertyChanged(); }
        }

        private bool _isChecking = false;
        public bool IsChecking
        {
            get => _isChecking;
            set { _isChecking = value; OnPropertyChanged(); }
        }


        public SettingsViewModel()
        {
            CheckForUpdatesCommand = new Command(async () => await CheckForUpdates());
            OpenDownloadUrlCommand = new Command(async () => await OpenDownloadURL());
            UpdateStorageInfoCommand = new Command(UpdateStorageInfo);

            CheckUpdateText = _defaultUpdateCheckText;
            IsCheckForUpdates = SettingsService.GetCheckForUpdatesOnStart();
            IsHomepageAnimation = SettingsService.GetCheckForHomepageAnimation();
            UpdateStorageInfo();
        }

        private void UpdateStorageInfo()
        {
            var externalStorage = StorageService.GetExternalStorageInfo();
            var internalStorage = StorageService.GetInternalStorageInfo();

            ExternalStorageInfo = $"Externalstorage:\n{FormatBytes(externalStorage.Available)} / {FormatBytes(externalStorage.Total)}";
            InternalStorageInfo = $"Internalstorage:\n{FormatBytes(internalStorage.Available)} / {FormatBytes(internalStorage.Total)}";

            ExternalStorageProgress = 1 - ((double)externalStorage.Available / externalStorage.Total);
            InternalStorageProgress = 1 - ((double)internalStorage.Available / internalStorage.Total);
        }

        private string FormatBytes(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int counter = 0;
            decimal number = (decimal)bytes;
            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }
            return string.Format("{0:n1} {1}", number, suffixes[counter]);
        }

        private async Task OpenDownloadURL()
        {
            string url = "https://github.com/sera619/YTDownloader-Android/releases";
            Uri uri = new Uri(url);
            try
            {
                await Microsoft.Maui.ApplicationModel.Browser.OpenAsync(uri);
            }
            catch (Exception ex)
            {
                CheckUpdateText = $"An error occured while opening download url: {ex.Message}";
            }
        }

        private async Task CheckForUpdates()
        {
            CheckUpdateText = "Checking for updates, please wait...";
            IsChecking = true;
            bool updateAvailbe = await VersionService.CheckVersionAsync();
            if (updateAvailbe)
            {
                CheckUpdateText = "A new Update is available.\nClick the button below to download!";
                IsDownloadButtonVisible = true;
            }
            else
            {
                CheckUpdateText = "You have already installed the latest version!";
                IsDownloadButtonVisible = false;
            }
            IsChecking = false;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
