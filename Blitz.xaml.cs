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
        Config.CurrentDice = reader.ReadToEnd().Split('\n').Select(s=>s.Split(' ')).ToArray();
        return Config.CurrentDice;
    }
    public  Blitz()
	{
        Task.Run(Loaddict).Wait();
        Task.Run(Loaddice).Wait();
        InitializeComponent();

        Application.Current.Dispatcher.Dispatch(() =>
        {
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
                {
                    Button button = new()
                    {
                        BackgroundColor = Colors.Navy,
                        TextColor = Colors.Pink,
                        Text = Config.CurrentDice[3][2],
                        FontSize = 45
                    };
                    button.Clicked += (object sender, EventArgs e) => ((Button)sender).Text = Config.Lexicon.Count.ToString();
                    board.Add(button, i, j);
                }
        });
        

    }
}