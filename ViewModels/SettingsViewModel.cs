using Android.Provider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using YTDownloaderMAUI.Services;

namespace YTDownloaderMAUI.ViewModels
{
    internal class SettingsViewModel : INotifyPropertyChanged
    {
        public ICommand CheckForUpdatesCommand { get; }
        public ICommand OpenDownloadUrlCommand { get; }


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

        private const string _defaultUpdateCheckText = "Press the button to check...";

        private string _checkUpdateText = string.Empty;
        public string CheckUpdateText
        {
            get => _checkUpdateText;
            set
            {
                _checkUpdateText = value;
                OnPropertyChanged();
            }
        }

        private bool _isDownloadButtonVisible = false;
        public bool IsDownloadButtonVisible
        {
            get => _isDownloadButtonVisible;
            set
            {
                _isDownloadButtonVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isChecking = false;
        public bool IsChecking
        {
            get => _isChecking;
            set
            {
                _isChecking = value;
                OnPropertyChanged();
            }
        }

        public SettingsViewModel()
        {
            CheckForUpdatesCommand = new Command(async () => await CheckForUpdates());
            OpenDownloadUrlCommand = new Command(async () => await OpenDownloadURL());
            CheckUpdateText = _defaultUpdateCheckText;
            IsCheckForUpdates = SettingsService.GetCheckForUpdatesOnStart();
            IsHomepageAnimation = SettingsService.GetCheckForHomepageAnimation();
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
