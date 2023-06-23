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
            int[] diceShuffleArray = Enumerable.Range(0, 16).OrderBy(lambda => Global.random.Next()).ToArray();
            int[] diceOrientationArray = new int[16].Select(lambda => Global.random.Next() % 6).ToArray();
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
            {
                gridLayout[j, i] = Dice.dice[diceShuffleArray[j * 4 + i]][diceOrientationArray[j * 4 + i]];

                Button label = new()
                {
                    Text = gridLayout[j, i],
                    TextColor = Colors.White,
                    BackgroundColor = Colors.Navy,
                    FontSize = 40,
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
            Console.WriteLine($"frontend sent {gridLayout[j, i]} , {(i,j)}");
            Submit.Letter(gridLayout[j,i], new Tuple<int, int>(i,j) );
        }

        private static void OnGridButtonPanned(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Completed) { Console.WriteLine("Submitted word"+Submit.Word()); return; }
            if (e.StatusType == GestureStatus.Started) { Console.WriteLine("swipe started"); Submit.Word();  return; }
            if (SwipeLogic.GetPosition(sender, e) != null)
            {
                Tuple<int, int> position = SwipeLogic.GetPosition(sender, e);
                (int i, int j) = position;
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
                        Opacity = 0.25,
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
