using System;
using System.Collections.Specialized;
using System.Text;
using WordBlitz.tools;

namespace WordBlitz.Screens.BlitzScreen;

public partial class BlitzScreen : ContentPage
{
    private string backgroundPath = BackgroundsMapping.getBackgroundFilename(Config.backgroundConfig);

    public BlitzScreen() //contsructor
	{
        InitializeComponent();
        blitzScreenBackgroundView.Source = backgroundPath;

        if (Config.tileSelectionMode > (int)TileSelectionMode.SwipeTapManualSubmit){ submitButton.IsVisible = true; submitButton.IsEnabled = false; }
        else { submitButton.IsVisible = true; submitButton.IsEnabled = true; }

        Submit.Bind(submitButton);
        Dice.Wait();
        Dispatcher.Dispatch(() => boardInitialiser.InitialiseBoard(boardGrid));

        Navigation.PushAsync(Settings.Get());
    }

    private void OnSubmitButtonTapped(object sender, TappedEventArgs e) { Submit.Word(); }
    private void OnSubmisButtonSwiped(object sender, SwipedEventArgs e) { Submit.Word(); }
}