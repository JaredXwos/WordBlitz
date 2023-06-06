using System;
using System.Collections.Specialized;
using System.Text;

namespace WordBlitz;

public partial class Blitz : ContentPage
{
    public  Blitz()
	{
        Task.Run(async () =>
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("CSW22.txt");
            using var reader = new StreamReader(stream);
            Config.Lexicon = new HashSet<string>(reader.ReadToEnd().Split('\n'));
        }).Wait();
        InitializeComponent();

        
        for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
        {
            Button button = new()
            {
                BackgroundColor = Colors.Navy,
                TextColor = Colors.Pink,
                Text = "Qu",
                FontSize = 45
            };
            button.Clicked += (object sender, EventArgs e) => ((Button)sender).Text = Config.Lexicon.Count.ToString();
            Application.Current.Dispatcher.Dispatch(() => board.Add(button, i, j));
        }
        

    }
}