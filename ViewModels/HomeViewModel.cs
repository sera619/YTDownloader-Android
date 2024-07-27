using System.Windows.Input;

namespace YTDownloaderMAUI.ViewModels
{
    public class HomeViewModel : BindableObject
    {
        public ICommand StartUpCommand { get; }

        public HomeViewModel()
        {
            StartUpCommand = new Command(() => Startup());
        }


        private async void Startup()
        {
            await Shell.Current.GoToAsync("//DownloadPage");
        }

    }
}
