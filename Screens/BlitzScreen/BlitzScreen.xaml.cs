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

        if (Load.Get()) { Navigation.PushAsync(Settings.Get()); }
        else { 
            IDispatcherTimer timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromSeconds(Config.blitzTimeConfig);
            timer.IsRepeating = false;
            timer.Tick += OnTimeOut;
            timer.Start();
        }
    }

    private void OnSubmitButtonTapped(object sender, TappedEventArgs e) { Submit.Word(); }
    private void OnSubmisButtonSwiped(object sender, SwipedEventArgs e) { Submit.Word(); }
    private void OnTimeOut(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Analysis());
        submitButton.Text = Submit.TotalUp().ToString();
    }
}