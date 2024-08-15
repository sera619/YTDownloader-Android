using System.Windows.Input;

namespace YTDownloaderMAUI.ViewModels
{
    public class HomeViewModel : BindableObject
    {
        public ICommand StartUpCommand { get; }

        private readonly string _baseSignatureText = "App design & development © 2024 S3R43o3";
        private string _signatureText = string.Empty;
        public string SignatureText
        {
            get => _signatureText;
            set => _signatureText = value;
        }

        public HomeViewModel()
        {
            StartUpCommand = new Command(() => Startup());
            SignatureText = $"Version {VersionTracking.CurrentVersion}\n{_baseSignatureText}";
        }

        private async void Startup()
        {
            await Shell.Current.GoToAsync("//DownloadPage");
        }

    }
}
