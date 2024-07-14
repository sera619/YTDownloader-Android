using CommunityToolkit.Maui.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace YTDownloaderMAUI.Views;

public partial class YTPopup : Popup, INotifyPropertyChanged
{
    private string title = string.Empty;
    private string message = string.Empty;
    private string okButtonText= string.Empty;
    private string cancelButtonText = string.Empty;

    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }

    public string Message
    {
        get => message;
        set => SetProperty(ref message, value);
    }

    public string OKButtonText
    {
        get => okButtonText;
        set => SetProperty(ref okButtonText, value);
    }

    
    public string CancelButtonText
    {
        get => cancelButtonText;
        set => SetProperty(ref cancelButtonText, value);
    }

    public YTPopup(string title, string message, string okButtonText = "OK", string cancelButtonText = "Cancel")
    {
        InitializeComponent();
        BindingContext = this;

        Title = title;
        Message = message;
        OKButtonText = okButtonText;
        CancelButtonText = cancelButtonText;
    }

    async void OnNoButtonClicked(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(false, cts.Token);
    }

    async void OnOKButtonClicked(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(true, cts.Token);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
