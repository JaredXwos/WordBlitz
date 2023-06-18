using System;
using System.Collections.Specialized;
using System.Text;
using WordBlitz.tools;

namespace WordBlitz.Screens.BlitzScreen;

public partial class BlitzScreen : ContentPage
{
    private static async Task<HashSet<string>> Loaddict()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(Config.dictionaryConfig);
        using var reader = new StreamReader(stream);
        Config.currentDict = new HashSet<string>(reader.ReadToEnd().Split('\n'));
        return Config.currentDict;
    }

    private static async Task<string[][]> Loaddice()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(Config.diceTypeConfig);
        using var reader = new StreamReader(stream);
        Config.currentDice = reader.ReadToEnd().Split('\n').Select(s=>s.Split(' ').Select(r=>r.Trim()).ToArray()).ToArray();
        return Config.currentDice;
    }

    public static string selectedword = "";
    private List<string> words = new();
    private string backgroundPath = BackgroundsMapping.getBackgroundFilename(Config.backgroundConfig);

    public BlitzScreen() //contsructor
	{
        Task dictloading = Loaddict();
        Task diceloading = Loaddice();

        InitializeComponent();
        blitzScreenBackgroundView.Source = backgroundPath;
        diceloading.Wait();
        BlitzScreenGrid.InitialiseBoard(boardGrid);

        IDispatcherTimer timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(Config.blitzTimeConfig);
        timer.Tick += (object sender, EventArgs e) =>
        {
            dictloading.Wait();
            Navigation.PushAsync(new AnalysisScreen());
            Submitted.Text = string.Empty;
            foreach (Button child in boardGrid.Children)
            {
                child.IsEnabled = false;
                child.BackgroundColor = Colors.Navy;
            }

            int points = 0;
            IEnumerable<string> validwords = Config.currentDict.Intersect(words).Where(c => c.Length > 2);
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
            testbutton.Text = (points - words.Count + validwords.Count()).ToString();
        };
        timer.Start();
    }

    private void OnSwiped(object sender, SwipedEventArgs e)
    {
        Submitted.Text = selectedword;
        words.Add(selectedword);
        Config.submittedWords.Add(selectedword);
        selectedword = string.Empty;
        foreach (Button child in boardGrid.Children)
        {
            child.IsEnabled = true;
            child.BackgroundColor = Colors.Navy;
        }
    }

    private void testbutton_Clicked(object sender, EventArgs e) => ((Button)sender).BackgroundColor = Colors.Red;

    
}