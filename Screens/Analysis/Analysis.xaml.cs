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
        foreach ((string word, int points) in SubmittedList.list)
        {
            analysisList.Add(word);
        }
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
            this.StrokeThickness = 2;
            this.Stroke = Colors.Black;
            VerticalStackLayout wordGroup = new VerticalStackLayout() 
            {
                Margin = 1,
                BackgroundColor = Colors.AliceBlue,
            };
            this.Content = wordGroup;
            
            int itemNumber = 0;
            Button[] buttons = new Button[5];
            Label[] labels = new Label[5];
            foreach (string word in wordsParam)
            {
                if (word != null)
                {
                    Button labelHitbox = new Button()
                    {
                        HeightRequest = 35,
                        VerticalOptions = LayoutOptions.Fill,
                        BackgroundColor = Color.FromRgba(90, 238, 90, 0),

                        Text = word,
                        TextColor = Colors.Black,
                        FontSize = 12,
                        
                        Opacity = 1,
                        CornerRadius = 0,
                    };
                    int itemIndex = itemNumber;
                    buttons[itemNumber] = labelHitbox;
                    labelHitbox.Pressed +=  (s, e) => { tapToToggle(s, e); };

                    wordGroup.Add(labelHitbox/*,0,itemNumber*/);

                    itemNumber ++;
                }
                else { break; };
            }
        }

        private void tapToToggle(object sender, EventArgs e)
        {
            var button = (Button)sender;
            Console.WriteLine(button.BackgroundColor.Green >= (float)0.30);
            button.BackgroundColor = (button.BackgroundColor.Green >= (float)0.5) ? Color.FromRgba(0.93, 0.35, 0.35, 0.5) : Color.FromRgba(0.35, 0.93, 0.35, 0.5);
            SubmittedList.Toggle(button.Text);
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