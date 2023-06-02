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
    }
    private void LetterSelected(object sender, EventArgs e)
    {
        ((Button)sender).Text = Config.Lexicon.Count.ToString();
    }
}