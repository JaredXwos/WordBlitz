namespace WordBlitz;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
    }
    private void ConfigUpdate(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex != -1) Config.Dict = (string)picker.ItemsSource[selectedIndex];
    }
}