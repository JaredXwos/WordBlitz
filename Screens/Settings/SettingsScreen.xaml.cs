using WordBlitz.tools;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace WordBlitz;

public partial class SettingsScreen : ContentPage
{
    
	public SettingsScreen()
	{
		InitializeComponent();
        dictPicker.SelectedItem             = Config.dictionaryConfig;
        dicePicker.SelectedItem             = Config.diceTypeConfig;
        backgroundPicker.SelectedItem       = Config.backgroundConfig;
        durationEntry.Text            = Config.blitzTimeConfig.ToString();
        tileSelectionPicker.SelectedIndex   = Config.tileSelectionMode;
        gameModePicker.SelectedItem               = Config.gamemodeConfig;
    }
    private void ConfigUpdate(object sender, EventArgs e)
    {
        Picker picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;
        if (selectedIndex != -1) switch (picker.StyleId)
        {
            case "dictPicker":
            {
                Config.dictionaryConfig = (string)picker.ItemsSource[selectedIndex];
                Preferences.Default.Set("dictionaryConfig", Config.dictionaryConfig);
                Dispatcher.Dispatch(Dict.Wait);
                Dict.Start();
                break;
            }
            case "dicePicker":
            {
                Config.diceTypeConfig = (string)picker.ItemsSource[selectedIndex];
                Preferences.Default.Set("diceTypeConfig", Config.diceTypeConfig);
                Dispatcher.Dispatch(Dice.Wait);
                Dice.Start();
                break;
            }
                
            case "backgroundPicker":
            {
                Config.backgroundConfig = (string)picker.ItemsSource[selectedIndex];
                Preferences.Default.Set("backgroundConfig", Config.backgroundConfig);
                break;
            }

            case "tileSelectionPicker":
            {
                Config.tileSelectionMode = picker.SelectedIndex;
                Preferences.Default.Set("TileSelectionMode", Config.tileSelectionMode);
                break;
            }
            case "gameModePicker":
            {
                Config.gamemodeConfig = (string)picker.ItemsSource[selectedIndex];
                Preferences.Default.Set("gameModeConfig", Config.gamemodeConfig);
                break;
            }
            default: break;
        }
    }

    private void SliderChanged(object sender, ValueChangedEventArgs e)
    {
        switch (((Slider)sender).StyleId)
        {
            case "durationSlider":
            {
                Config.blitzTimeConfig = (int)e.NewValue;
                Preferences.Default.Set("blitzTimeConfig", Config.blitzTimeConfig);
                durationEntry.Text = Config.blitzTimeConfig.ToString();
                break;
            }
            default: break;
        }
    }

    private void Entry_TextChanged(object sender, EventArgs e)
    {
        Entry entry = (Entry)sender;
        switch (entry.StyleId)
        {
            case "durationEntry":
            {
                durationSlider.Value = Math.Max(1,Math.Min(600, Int32.Parse(entry.Text)));
                durationEntry.Text = ((int) durationSlider.Value).ToString();
                break;
            }
            default: break;
        }
    }
}