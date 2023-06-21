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

        Dice.Wait();
        Dispatcher.Dispatch(() => boardInitialiser.InitialiseBoard(boardGrid));

        IDispatcherTimer timer = Dispatcher.CreateTimer();
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
        timer.Start();
    }

    private void OnSubmitButtonTapped(object sender, TappedEventArgs e) { Console.WriteLine("Submit Was called from blitzScreen.xaml.cs,needs uncommenting");/*Submit.Word();*/ }
    private void OnSubmisButtonSwiped(object sender, SwipedEventArgs e) { Console.WriteLine("Submit Was called from blitzScreen.xaml.cs,needs uncommenting");/*Submit.Word();*/  }

}