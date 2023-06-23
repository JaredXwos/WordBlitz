using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Text;
using WordBlitz.tools;

namespace WordBlitz;

public partial class Analysis : ContentPage
{
    public static List<string> analysisList = new List<string>(); //TODO README make this object reference the same list from blitzscreen.
    
    public Analysis()
    {
        InitializeComponent();
        Dispatcher.Dispatch(() =>
        {
            
            foreach ((string word, int points) in SubmittedList.list)
            {
                analysisList.Add(word);
                Button button = new Button()
                {
                    Text = word,
                    TextColor = Colors.White,
                    BackgroundColor = Colors.Navy
                };
                /*button.Clicked += OnClick;*/
                Display.Add(button);
            }
        });
        /*makes a long list for testing*/analysisList = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50" };
        DualView dualView = new DualView();
        Display.Add(dualView);

        generateCards(analysisList, dualView);
    }

    private void OnSubmit(object sender, EventArgs e) { Score.Text = SubmittedList.Total().ToString(); }

    private void generateCards(List<string> list,DualView dualView)
    {
        double groups = Math.Ceiling((double)list.Count / (double)5);
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
            this.BackgroundColor = Colors.Black;
            this.StrokeThickness = 5;
            Grid wordGroup = new Grid()
            {
               Margin = 1,
               RowSpacing = 0,
               BackgroundColor = Colors.AliceBlue,
               RowDefinitions = new RowDefinitionCollection()
               {
                   new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
                   new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
                   new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
                   new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
                   new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
               },
            };
            this.Content = wordGroup;
            
            int itemNumber = 0;
            Button[] buttons = new Button[5];
            Label[] labels = new Label[5];
            foreach (string word in wordsParam)
            {
                if (word != null)
                {
                    Label textlabel = new Label()
                    {
                        HeightRequest = 30,
                        VerticalOptions = LayoutOptions.Fill,
                        BackgroundColor = Colors.Transparent,

                        Text = word,
                        FontSize = 15,
                        VerticalTextAlignment = TextAlignment.Center,
                        
                    };
                    labels[itemNumber] = textlabel;

                    Button labelHitbox = new Button()
                    {
                        HeightRequest = 30,
                        VerticalOptions = LayoutOptions.Fill,
                        BackgroundColor = Colors.Transparent,

                        Opacity = 0.1,
                        CornerRadius = 0,
                    };
                    buttons[itemNumber] = labelHitbox;
                    labelHitbox.Pressed +=  (s, e) => { tapToToggle(s, e); Console.WriteLine((cardnumber,itemNumber)); };


                    wordGroup.Add(textlabel,0,itemNumber);
                    wordGroup.Add(labelHitbox,0,itemNumber);

                    itemNumber ++;
                }
                else { break; };
            }
        }

        private void tapToToggle(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.BackgroundColor = (button.BackgroundColor == Colors.Green) ? Colors.Red : Colors.Green;
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