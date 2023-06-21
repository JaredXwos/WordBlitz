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
        //initialisation
        InitializeComponent();
        blitzScreenBackgroundView.Source = backgroundPath;

        Dice.Wait();
        
        Dispatcher.Dispatch( () => BoardInitialiser.InitialiseBoard(boardGrid));


        IDispatcherTimer timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(Config.blitzTimeConfig);
        //--------------------------




        //we gonna have to redo this part anyways
        timer.Tick += (object sender, EventArgs e) =>
        {
            Dict.Wait();
            Navigation.PushAsync(new Analysis());
            /*Submitted.Text = string.Empty;
            foreach (Button child in boardGrid.Children)
            {
                child.IsEnabled = false;
                child.BackgroundColor = Colors.Navy;
            }

            int points = 0;
            IEnumerable<string> validwords = Dict.dict.Intersect(BlitzData.getList).Where(c => c.Length > 2);
            foreach (string word in validwords) // we will count score async in real time
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
                Submitted.Text += word;
                Submitted.Text += ' ';
            }
            testbutton.Text = (points - Submit.Getlist().Count + validwords.Count()).ToString();*/
        };
        timer.Start();


    }


    //event handlers
    private static void OnGridButtonPanned(object sender, PanUpdatedEventArgs e)
    {
        BlitzEventHandlers.processPanInfomationHandler(sender, e);
    }

    private static void OnGridButtonTapped(object sender, TappedEventArgs e)
    {
        var label = (Label)sender;
        label.IsEnabled = false;
        BlitzEventHandlers.addLetterHandler(sender);
        label.IsEnabled = true;
    }

    private static void OnSubmitButtonTapped(object sender, TappedEventArgs e)
    {
        var label = (Label)sender;
        label.IsEnabled = false;
        BlitzEventHandlers.submissionHandler();
        label.IsEnabled = true;
        Console.WriteLine("SUBMIT");
    }
}

    
/*
    private void OnSwiped(object sender, SwipedEventArgs e) { Submitted.Text = Submit.Word(); }
    private void testbutton_Clicked(object sender, EventArgs e) => ((Button)sender).BackgroundColor = Colors.Red;}*/