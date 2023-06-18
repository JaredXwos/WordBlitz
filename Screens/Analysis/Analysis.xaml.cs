using WordBlitz.tools;

namespace WordBlitz;

public partial class AnalysisScreen : ContentPage
{
    public AnalysisScreen()
	{
        InitializeComponent();
		Dispatcher.Dispatch(()=> { foreach (string word in Global.submittedWords) Display.Add(new Button { Text = word }); });
		
	}
}