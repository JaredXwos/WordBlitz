using WordBlitz.Screens.BlitzScreen;

namespace WordBlitz;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void OnPlayButtonClicked(object sender, EventArgs e)
    {
        blitzScreenNavButton.IsEnabled = false;
        await Navigation.PushAsync(new BlitzScreen());
        blitzScreenNavButton.IsEnabled = true;
    }

    private async void OnAnalysisButtonClicked(object sender, EventArgs e)
    {
        analysisScreenNavButton.IsEnabled = false;
        await Navigation.PushAsync(new AnalysisScreen());
        analysisScreenNavButton.IsEnabled = true;
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        settingsScreenNavButton.IsEnabled = false;
        await Navigation.PushAsync(new SettingsScreen());
        settingsScreenNavButton.IsEnabled = true;
    }
}

