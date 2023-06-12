using System;
using System.Collections.Specialized;
using System.Text;

namespace WordBlitz;

public partial class Blitz : ContentPage
{
    private static async Task<HashSet<string>> Loaddict()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(Config.DictName);
        using var reader = new StreamReader(stream);
        Config.Lexicon = new HashSet<string>(reader.ReadToEnd().Split('\n'));
        return Config.Lexicon;
    }

    private static async Task<string[][]> Loaddice()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(Config.DiceName);
        using var reader = new StreamReader(stream);
        Config.CurrentDice = reader.ReadToEnd().Split('\n').Select(s=>s.Split(' ').Select(r=>r.Trim()).ToArray()).ToArray();
        return Config.CurrentDice;
    }

    private string selectedword = "";

    public  Blitz()
	{
        Task.Run(Loaddict).Wait();
        Task.Run(Loaddice).Wait();
        InitializeComponent();

        Dispatcher.Dispatch(() =>
        {
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
            {
                Button button = new()
                {
                    BackgroundColor = Colors.Navy,
                    FontSize = 40,
                    Text = Config.CurrentDice[i * 4 + j][Config.Random.Next() % 6]
                };
                button.Pressed += (object sender, EventArgs e) =>
                {
                    Button button = (Button)sender;
                    if (button.IsEnabled)
                    {
                        selectedword += button.Text;
                        button.BackgroundColor = Colors.Black;
                        foreach (Button child in board.Children) if(child.BackgroundColor != Colors.Black) child.IsEnabled = true;
                        foreach (Button child in board.Children.Where(c => 
                            board.GetRow(c) < board.GetRow(button) - 1 ||
                            board.GetRow(c) > board.GetRow(button) + 1 ||
                            board.GetColumn(c) < board.GetColumn(button) - 1 ||
                            board.GetColumn(c) > board.GetColumn(button) + 1
                        )) child.IsEnabled = false;
                    }
                };
                button.Released += (object sender, EventArgs e) =>
                {
                    Button button = (Button)sender;
                    if (selectedword != "") button.IsEnabled = false;
                };
                
                board.Add(button, i, j);
            }
        });
        IDispatcherTimer timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(180);
        timer.Tick += (object sender, EventArgs e) => Navigation.PopAsync();
        timer.Start();
    }

    private void OnSwiped(object sender, SwipedEventArgs e)
    {
        Submitted.Text = selectedword;
        selectedword = string.Empty;
        foreach (Button child in board.Children)
        {
            child.IsEnabled = true;
            child.BackgroundColor = Colors.Navy;
        }
    }

    private void testbutton_Clicked(object sender, EventArgs e) => ((Button)sender).BackgroundColor = Colors.Red;
}