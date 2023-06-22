using WordBlitz.Screens.BlitzScreen;
using WordBlitz.tools;
namespace WordBlitz;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        Global.Init();
	}

    private async void OnPlayButtonClicked(object sender, EventArgs e)
    {
        blitzScreenNavButton.IsEnabled = false;
        await Navigation.PushAsync(new BlitzScreen());
        blitzScreenNavButton.IsEnabled = true;
    }

    private void OnAnalysisButtonClicked(object sender, EventArgs e)
    {
        analysisScreenNavButton.IsEnabled = false;
        analysisScreenNavButton.IsEnabled = true;
    }

    private async void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        settingsScreenNavButton.IsEnabled = false;
        await Navigation.PushAsync(new SettingsScreen());
        settingsScreenNavButton.IsEnabled = true;
    }
}

