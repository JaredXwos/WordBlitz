using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Text;
using WordBlitz.tools;

namespace WordBlitz;

public partial class Analysis : ContentPage
{
    public static List<string> analysisList = new List<string>(); //TODO README make this object reference the same list from blitzscreen.
    public static Label[][] allButtons;  //allButtons[cardnumber][itemNumber]

    public Analysis()
    {
        InitializeComponent();
        foreach ((string word, int points) in SubmittedList.list)
        {
            analysisList.Add(word);
        }
        DualView dualView = new DualView();
        Display.Add(dualView);

        generateCards(analysisList, dualView);
    }

    private void OnSubmit(object sender, EventArgs e)
    {
        foreach (Label[] buttons in allButtons) foreach (Label button in buttons) button.IsEnabled = false;

        Button submitButton = (Button)sender;
        Score.Text = "Score: " + SubmittedList.Total().ToString();
        Score.IsVisible = true;
        submitButton.Text = "exit to main menu";
        submitButton.Pressed += ExitToMainMenu;
    }

    private async void ExitToMainMenu(object sender , EventArgs e)
    {
        Navigation.RemovePage(Blitz.Get());
        await Navigation.PopAsync();
        await Console.Out.WriteLineAsync("line 55 analysis.xaml.cs, add additional pop logic for after analysis screen");
    }


    private void generateCards(List<string> list,DualView dualView)
    {
        int groups = (int) Math.Ceiling((double)list.Count / (double)5);
        allButtons = new Label[groups][];
        bool alternateLeftRight = true;
        for (int i =0; i<groups; i++)
        {
            string[] group = analysisList.Skip(5*i).Take(5).ToArray();
            if (alternateLeftRight) { dualView.addToCol(new AnalysisCard(group , i), 0); alternateLeftRight = false; }
            else { dualView.addToCol(new AnalysisCard(group , i) , 1); alternateLeftRight = true; }
        }
    }


    private class AnalysisCard : Border
    {
        public AnalysisCard(string[] wordsParam , int cardnumber)
        {
            this.StrokeThickness = 2;
            this.Stroke = Colors.Black;
            VerticalStackLayout wordGroup = new VerticalStackLayout()
            {
                Margin = 1,
                BackgroundColor = Colors.AliceBlue,
                Padding = 8
            };
            this.Content = wordGroup;
            int itemNumber = 0;
            allButtons[cardnumber] = new Label[wordsParam.Length];
            foreach (string word in wordsParam)
            {
                if (word != null)
                {
                    Label labelHitbox = new()
                    {
                        VerticalOptions = LayoutOptions.Fill,
                        BackgroundColor = Color.FromRgba(90, 238, 90, 0.2),
                        Text = word,
                        TextColor = Colors.Black,
                        FontSize = 16,
                        Opacity = 1
                    };
                    int itemIndex = itemNumber;
                    allButtons[cardnumber][itemNumber] = labelHitbox;
                    TapGestureRecognizer OnTapped = new();
                    OnTapped.Tapped += tapToToggle;
                    labelHitbox.GestureRecognizers.Add(OnTapped);

                    wordGroup.Add(labelHitbox);

                    itemNumber ++;
                }
                else { break; };
            }
        }

        private void tapToToggle(object sender, EventArgs e)
        {
            Label button = (Label)sender;
            button.TextDecorations = SubmittedList.Toggle(button.Text) ? TextDecorations.None : TextDecorations.Strikethrough;
        }

    }

    private class DualView : Grid
    {
        VerticalStackLayout column0 = new();
        VerticalStackLayout column1 = new();
        public DualView()
        {
            this.ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition{ Width = new GridLength(2, GridUnitType.Star)},
                new ColumnDefinition{ Width = new GridLength(2, GridUnitType.Star)},
            };
            this.Add(column0,0,0);
            this.Add(column1,1,0);
            
        }

        public void addToCol( AnalysisCard card , int columnIndex)
        {
            if (columnIndex == 0)
            {
                column0.Add(card);
                return;
            }
            else if (columnIndex == 1)
            {
                column1.Add(card);
                return;
            }
            else throw new Exception($"exception in analysis.xaml.cs column number in this class not expected");
        }
    }







}