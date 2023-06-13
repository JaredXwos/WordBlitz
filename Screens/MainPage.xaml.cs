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

    private void OnPlayClicked(object sender, EventArgs e)
    {
<<<<<<< HEAD:Screens/MainPage.xaml.cs
        Navigation.PushAsync(new BlitzScreen());
=======
        Navigation.PushAsync(new Analysis());
>>>>>>> master:MainPage.xaml.cs
    }
}

