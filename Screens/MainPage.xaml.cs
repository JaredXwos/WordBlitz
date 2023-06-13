namespace WordBlitz;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new SettingsScreen());
	}

    private async void OnPlayButtonClicked(object sender, EventArgs e)
    {
        BlitzScreenNavButton.IsEnabled = false;
        await Navigation.PushAsync(new BlitzScreen());
        BlitzScreenNavButton.IsEnabled = true;
    }

    private async void OnAnalysisButtonClicked(object sender, EventArgs e)
    {
        AnalysisScreenNavButton.IsEnabled = false;
        await Navigation.PushAsync(new AnalysisScreen());
        AnalysisScreenNavButton.IsEnabled = true;
    }
}

