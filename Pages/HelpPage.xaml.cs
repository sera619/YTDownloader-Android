using System.Runtime.CompilerServices;

namespace YTDownloaderMAUI.Pages;

public partial class HelpPage : ContentPage
{

    private string[] useInstructions = new string[]
    {
         "• Open a video you want to download in the youtube app",
        "• click on the 'share' button",
        "• use the 'copy url/link' button in the following menu",
        "• switch to the YT Downloader app",
        "• in the download menu you can use the paste button to paste the url into the input field",
        "• then click the add button and the video will be added to the download waiting list (you can have multiple videos in the waiting list)",
        "• you are ready to go, press the download button to start the process"
    };
    private bool _isUsageHelpCreated = false;

    public HelpPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // create helptext as page appears
        if (!_isUsageHelpCreated) 
        {
            CreateUsageHelp();
            _isUsageHelpCreated = true;
        }
    }

    private void CreateUsageHelp()
	{
        foreach (var instruction in useInstructions) 
        {
            Label newLabel = new Label
            {
                Text = instruction,
                Style = (Style)Application.Current.Resources["BodyLabelStyle"]
            };
		    UsageHelpLayout.Children.Add(newLabel);
        }
    }

    private async void GoToAboutButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//AboutPage");
    }
}