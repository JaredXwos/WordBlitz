namespace WordBlitz;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
        Dictpicker.SelectedItem = Config.DictName;
        Dicepicker.SelectedItem = Config.DiceName;
    }
    private void ConfigUpdate(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex != -1) switch (picker.StyleId)
        {
            case "Dictpicker":
                Config.DictName = (string)picker.ItemsSource[selectedIndex]; break;
            case "Dicepicker":
                Config.DiceName = (string)picker.ItemsSource[selectedIndex]; break;
            default: break;
        }
    }
}