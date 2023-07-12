using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBlitz.tools;

namespace WordBlitz.Screens.BlitzScreen
{
    public static class boardInitialiser
    {
        public static string[,] gridLayout = new string[4, 4];
        private static volatile Queue<Tuple<string, Tuple<int, int>>> requestqueue = new();
        static internal void InitialiseBoard(Grid board)
        {
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
            {
                Button label = new()
                {
                    TextColor = Colors.White,
                    BackgroundColor = Colors.Navy,
                    FontSize = 30,
                    CornerRadius = 4000,
                    WidthRequest = board.HeightRequest/4,
                    HeightRequest = board.HeightRequest/4,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Scale = 1,
                    Opacity = 1,
                };
                board.Add(label, j, i);

                Grid hitboxGrid = GenerateHitBoxes(board.HeightRequest / 4);
                board.Add(hitboxGrid, j, i);
            }
        }
        private static void OnButtonPressed(object sender, EventArgs e)
        {
            var hitboxLabel = (Label)sender;  // might cause type casting errors
            var hitboxGrid = (Grid)hitboxLabel.Parent;
            var boardGrid = (Grid)hitboxGrid.Parent;
            (int i, int j) = (boardGrid.GetRow(hitboxGrid), boardGrid.GetColumn(hitboxGrid));
            Console.WriteLine(gridLayout[j,i] + "was tapped");
            Submit.Letter(gridLayout[i,j], new Tuple<int, int>(j,i) );
        }

        private static void OnGridButtonPanned(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Completed) { Console.WriteLine("Submitted word"+Submit.Word()); return; }
            if (e.StatusType == GestureStatus.Started) { Console.WriteLine("swipe started"); Submit.Word();  return; }
            if (SwipeLogic.GetPosition(sender, e) != null)
            {
                Tuple<int, int> position = SwipeLogic.GetPosition(sender, e);
                (int j, int i) = position;
                Console.WriteLine($"submitted letter = {gridLayout[j, i]}");
                Console.WriteLine($"(coords: {i} {j}, row={i} col={j}) from boardinitialiser, awaiting uncommenting");
                Submit.Letter(gridLayout[j, i], position);
            };
        }

        private static Grid GenerateHitBoxes( double heightParam)
        {
            Console.WriteLine($"height param is {heightParam}");
            Grid board = new()
            { 
                HeightRequest = heightParam,
                WidthRequest = heightParam,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                },
            };
            

            Label[,] labels = new Label[3,3];

            for (int i = 0; i < 3; i++) for (int j = 0; j < 3; j++)
                {
                    labels[i, j] = new Label()
                    {
                        BackgroundColor = Colors.Green,
                        Opacity = 0,
                    };
                    board.Add(labels[i,j],i,j);
                }
            labels[1,1].BackgroundColor = Colors.Red;

            for (int i = 0;i < 3;i++) for (int j = 0;j<3;j++)
                {
                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) => { OnButtonPressed(s, e); };
                    PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
                    panGestureRecognizer.PanUpdated += (s, e) => { OnGridButtonPanned(s, e); };
                    if (Config.tileSelectionMode != (int)TileSelectionMode.SwipeOnly) { labels[i, j].GestureRecognizers.Add(tapGestureRecognizer); }
                    if (Config.tileSelectionMode != (int)TileSelectionMode.TapOnly  ) { labels[i, j].GestureRecognizers.Add(panGestureRecognizer); }
                }
            return board;
        }

    }
}
