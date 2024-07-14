namespace YTDownloaderMAUI.Views;


public partial class PageHeaderView : ContentView
{
    public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(
        nameof(HeaderText),
        typeof(string),
        typeof(PageHeaderView),
        string.Empty);

    public string HeaderText
    {
        get => (string)GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    public PageHeaderView()
    {
        InitializeComponent();
        this.BindingContext = this;
    }
}
