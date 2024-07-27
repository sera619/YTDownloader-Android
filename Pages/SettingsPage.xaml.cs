namespace YTDownloaderMAUI.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();

    }
    protected override bool OnBackButtonPressed()
    {
        // Optionally, you can add custom logic here
        return base.OnBackButtonPressed(); // Return true to handle the back navigation yourself, or false to use default behavior
    }
}