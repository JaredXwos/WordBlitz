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

    private void OnPlayButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new BlitzScreen());
    }

    private void OnAnalysisButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AnalysisScreen());
    }
}

