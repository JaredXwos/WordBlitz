using WordBlitz.tools;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace WordBlitz;

public partial class SettingsScreen : ContentPage
{
    
	public SettingsScreen()
	{
		InitializeComponent();
        Navigation.PushAsync(new MainPage());
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
        if(((Slider)sender).StyleId.Contains("Letter")) PointSliderChanged(sender, e);
        else switch (((Slider)sender).StyleId)
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
        if (entry.StyleId.Contains("Letter")) PointEntryChanged(sender, e);
        else switch (entry.StyleId)
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

    private void PointSliderChanged(object sender, ValueChangedEventArgs e)
    {
        Slider id = (Slider)sender;
        string[] full = id.StyleId.Split("Letter");
        int letter = 0;
        switch (full[0])
        {
            case "three": letter = 3; break;
            case "four": letter = 4; break;
            case "five": letter = 5; break;
            case "six": letter = 6; break;
            case "seven": letter = 7; break;
            case "above": letter = 8; break;
            default: break;
        }
        Points.Set(letter, full[1].Contains("Reward") ? "reward" : "penalty", (int)e.NewValue);
        ((Entry)((StackLayout)id.Parent).Children[0]).Text = ((int)id.Value).ToString();
    }

    private void PointEntryChanged(object sender, EventArgs e)
    {
        Entry entry = (Entry)sender;
        Slider slider = (Slider)((StackLayout)entry.Parent).Children[1];
        slider.Value = Math.Max(slider.Minimum, Math.Min(slider.Maximum, Int32.Parse(entry.Text)));
        entry.Text = ((int)slider.Value).ToString();
    }
}