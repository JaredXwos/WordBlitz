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


        //load submitbutton for tap mode ---------------------
        //Config.tileSelectionMode = 2; for testing purposes
        if (Config.tileSelectionMode > (int)TileSelectionMode.SwipeTapManualSubmit){ submitButton.IsVisible = false; submitButton.IsEnabled = false; }
        else { submitButton.IsVisible = true; submitButton.IsEnabled = true; }
        //--------------------------------

        //TODO Check Fun Mode to decide what to do with displaying score

        //TODO Check isShakeBoard[currently does not exist yet] enabled and act accordingly




        Dice.Wait();
        Dispatcher.Dispatch(() => boardInitialiser.InitialiseBoard(boardGrid));

        /*IDispatcherTimer timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(Config.blitzTimeConfig);
        timer.Tick += (object sender, EventArgs e) =>
        {
            Dict.Wait();
            Navigation.PushAsync(new Analysis());

            int points = 0;
            IEnumerable<string> validwords = Dict.dict.Intersect(Submit.Getlist()).Where(c => c.Length > 2);
            foreach (string word in validwords)
            {
                switch (word.Length)
                {
                    case 3: points++; break;
                    case 4: points++; break;
                    case 5: points += 2; break;
                    case 6: points += 3; break;
                    case 7: points += 5; break;
                    default: points += 11; break;
                }


            }


        };
        timer.Start();*/
    }

    private void OnSubmitButtonTapped(object sender, TappedEventArgs e) { Submit.Word(); }
    private void OnSubmisButtonSwiped(object sender, SwipedEventArgs e) { Submit.Word(); }

}