using WordBlitz.tools;

namespace WordBlitz;

public partial class AnalysisScreen : ContentPage
{
    public AnalysisScreen()
	{
        InitializeComponent();
		Dispatcher.Dispatch(()=> { foreach (string word in Submit.All()) Display.Add(new Button {
			Text = word,
			TextColor = Colors.White,
			BackgroundColor = Colors.Navy
		}); });
	}
}