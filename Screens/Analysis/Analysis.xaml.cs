using System;
using System.Collections.Specialized;
using System.Text;
using WordBlitz.tools;

namespace WordBlitz;

public partial class Analysis : ContentPage
{
    public Analysis()
    {
        InitializeComponent();
        Dispatcher.Dispatch(() =>
        {
            foreach ((string word, int points) in SubmittedList.list)
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
        button.BackgroundColor = SubmittedList.Toggle(button.Text) ? Colors.Navy : Colors.Black;
    }

    private void OnSubmit(object sender, EventArgs e) { Score.Text = SubmittedList.Total().ToString(); }
}