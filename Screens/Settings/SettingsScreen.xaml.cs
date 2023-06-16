using WordBlitz.tools;

namespace WordBlitz;

public partial class SettingsScreen : ContentPage
{
	public SettingsScreen()
	{
		InitializeComponent();
        dictPicker.SelectedItem       = Config.dictionaryConfig;
        dicePicker.SelectedItem       = Config.diceTypeConfig;
        backgroundPicker.SelectedItem = Config.backgroundConfig;
        Dispatcher.Dispatch(new Action(() =>
        {
            DurationSliderLabel.Text = "Set duration of blitz game: " + Config.blitzTimeConfig.ToString();
        }));
    }
    private void ConfigUpdate(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex != -1) switch (picker.StyleId)
        {
            case "dictPicker":
                Config.dictionaryConfig = (string)picker.ItemsSource[selectedIndex]; break;
            case "dicePicker":
                Config.diceTypeConfig = (string)picker.ItemsSource[selectedIndex]; break;
            case "backgroundPicker":
                Config.backgroundConfig = (string)picker.ItemsSource[selectedIndex]; break;
            default: break;
        }
    }
    private void blitzDurationChanged(object sender, ValueChangedEventArgs e)
    {
        Config.blitzTimeConfig = (int)e.NewValue;
        DurationSliderLabel.Text = "Set duration of blitz game: " + Config.blitzTimeConfig.ToString();
    }
}