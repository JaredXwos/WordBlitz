using WordBlitz.Screens.BlitzScreen;
using WordBlitz.tools;
namespace WordBlitz;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        Dispatcher.Dispatch(()=> {
            Global.Init();
            if (Load.Get())
            {
                Navigation.RemovePage(Blitz.Get());
                Navigation.RemovePage(Settings.Get());
            }
            Load.Toggle();
        });
    }

    private async void OnPlayButtonClicked(object sender, EventArgs e)
    {
        blitzScreenNavButton.IsEnabled = false;
        await Navigation.PushAsync(Blitz.Update());
        blitzScreenNavButton.IsEnabled = true;
    }

    private void OnAnalysisButtonClicked(object sender, EventArgs e)
    {
        analysisScreenNavButton.IsEnabled = false;
        Navigation.PushAsync(new BlitzScreen());
        analysisScreenNavButton.IsEnabled = true;
    }

    private void OnSettingsButtonClicked(object sender, EventArgs e)
    {
        settingsScreenNavButton.IsEnabled = false;
        Navigation.PushAsync(Settings.Update());
        settingsScreenNavButton.IsEnabled = true;
    }
}

