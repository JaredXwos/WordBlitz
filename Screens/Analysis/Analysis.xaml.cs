using System;
using System.Collections.Specialized;
using System.Text;
using WordBlitz.tools;

namespace WordBlitz;

public partial class Analysis : ContentPage
{
	private static SortedSet<string> submittedList = BlitzData.GetList;
	BlitzData.reset();
    public Analysis()
	{
        InitializeComponent();
		Dispatcher.Dispatch(() =>
		{
			foreach (string word in submittedList)
			{
				Button button = new Button()
				{
					Text = word,
					TextColor = Colors.White,
					BackgroundColor = Colors.Navy
				};
				button.Clicked += OnClick;
				Display.Add(button);
			}
		});
	}

	private void OnClick(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		if(submittedList.Contains(button.Text))
		{
			button.Background = Colors.Black;
			submittedList.Remove(button.Text);
			Score.Text = submittedList.Count.ToString();
        }
        else
		{
			button.Background = Colors.Navy;
			submittedList.Add(button.Text);
            Score.Text = submittedList.Count.ToString();
        }
	}
}