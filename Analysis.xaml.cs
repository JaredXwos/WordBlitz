namespace WordBlitz;

public partial class Analysis : ContentPage
{
    private static async Task<HashSet<string>> Loaddict()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync(Config.DictName);
        using var reader = new StreamReader(stream);
        Config.Lexicon = new HashSet<string>(reader.ReadToEnd().Split('\n'));
        return Config.Lexicon;
    }
    public Analysis()
	{
        Task.Run(Loaddict).Wait();
        InitializeComponent();
		Dispatcher.Dispatch(()=> { foreach (string word in Config.Lexicon.Take(100)) Display.Add(new Button { Text = word }); });
		
	}
}