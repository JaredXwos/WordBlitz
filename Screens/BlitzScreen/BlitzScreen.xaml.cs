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
        Dispatcher.Dispatch(() => BlitzScreenGrid.InitialiseBoard(boardGrid));

        IDispatcherTimer timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(Config.blitzTimeConfig);
        timer.Tick += (object sender, EventArgs e) =>
        {
            Dict.Wait();
            Global.selectedWord = "";
            Navigation.PushAsync(new AnalysisScreen());
            Submitted.Text = string.Empty;
            foreach (Button child in boardGrid.Children)
            {
                child.IsEnabled = false;
                child.BackgroundColor = Colors.Navy;
            }

            int points = 0;
            IEnumerable<string> validwords = Dict.dict.Intersect(Global.submittedWords).Where(c => c.Length > 2);
            foreach(string word in validwords)
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
            testbutton.Text = (points - Global.submittedWords.Count + validwords.Count()).ToString();
        };
        timer.Start();
    }

    private void OnSwiped(object sender, SwipedEventArgs e)
    {
        if(Global.selectedWord != "")
        {
            Submitted.Text = Global.selectedWord;
            Global.submittedWords.Add(Global.selectedWord);
            Global.selectedWord = string.Empty;
            foreach (Button child in boardGrid.Children)
            {
                child.IsEnabled = true;
                child.BackgroundColor = Colors.Navy;
            }
        }
    }
    private void testbutton_Clicked(object sender, EventArgs e) => ((Button)sender).BackgroundColor = Colors.Red;

    
}