namespace YTDownloaderMAUI.Views;


public partial class PageHeaderView : ContentView
{
    public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(
        nameof(HeaderText),
        typeof(string),
        typeof(PageHeaderView),
        string.Empty);

    private string _versionText = string.Empty;
    public string VersionText
    {
        get => _versionText;
        set
        {
            _versionText = value;
        }
    }
    public string HeaderText
    {
        get => (string)GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    public PageHeaderView()
    {
        InitializeComponent();
        VersionText = $"Version {VersionTracking.CurrentVersion}";
        this.BindingContext = this;
    }
}
