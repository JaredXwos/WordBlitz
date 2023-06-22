using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBlitz.Screens.BlitzScreen.SwipeLogicOld;
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

                    Button label = new Button()
                    {
                        Text = /*$"{j},{i}"*/gridLayout[j, i],
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

                    /*TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) => { OnButtonPressed(s, e); };
                    PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
                    panGestureRecognizer.PanUpdated += (s, e) => { OnGridButtonPanned(s, e); };
                    Label touchLabel = new Label()  // invisible label
                    {
                        BackgroundColor = Colors.Red,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        WidthRequest = board.HeightRequest*0.02,
                        HeightRequest = board.HeightRequest*0.02,
                        Scale = 2,
                        Opacity = 0.5,
                        GestureRecognizers = { panGestureRecognizer },
                    };

                    board.Add(touchLabel, j, i); //important: grid.add() is defined as grid.add(element,COL,row) and not vice versa, so for row=i , col =j its j,i here*/

                    Grid hitboxGrid = GenerateHitBoxes(board.HeightRequest / 4);
                    board.Add(hitboxGrid, j, i);
            }
        }
        private static void OnButtonPressed(object sender, EventArgs e)
        {
            var element = (Label)sender;  // might cause type casting errors
            var boardgrid = (Grid)element.Parent;
            Console.WriteLine($"frontend sent{element.Text} , ({boardgrid.GetRow(element)},{boardgrid.GetColumn(element)})");
            Submit.Letter(element.Text, new Tuple<int, int> (boardgrid.GetRow(element), boardgrid.GetColumn(element)));
        }

        private static void OnGridButtonPanned(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Completed) { Console.WriteLine("submit called from board intialiser,awaiting uncommenting")/*Submit.Letter(gridLayout[j, i], position);*/; return; }
            if (e.StatusType == GestureStatus.Started) { Console.WriteLine("swipe started"); return; }
            if (SwipeLogic.GetPosition(sender, e) != null)
            {
                Tuple<int, int> position = SwipeLogic.GetPosition(sender, e);
                (int i, int j) = position; // completed function, will not have errors here
            };
        }

        private static Grid GenerateHitBoxes( double heightParam)
        {
            Console.WriteLine($"height param is {heightParam}");
            Grid board = new Grid()
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
                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) => { OnButtonPressed(s, e); };
                    PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
                    panGestureRecognizer.PanUpdated += (s, e) => { OnGridButtonPanned(s, e); };
                    labels[i, j] = new Label()
                    {
                        BackgroundColor = Colors.Green,
                        Opacity = 0.25,
                        GestureRecognizers = { panGestureRecognizer },
                    };
                    board.Add(labels[i,j],i,j);
                }
            labels[1,1].BackgroundColor = Colors.Red;

            return board;
        }

    }
































    /*internal class BlitzScreenGrid
    {
        int[] diceShuffleArray = Enumerable.Range(0, 16).OrderBy(lambda => Config.random.Next()).ToArray()*//*Enumerable.Repeat(0, 16).ToArray();*//* ;
        int[] diceOrientationArray = new int[16].Select(lambda => Config.random.Next() % 6).ToArray()*//*Enumerable.Repeat(0, 16).ToArray();*//* ;
        BlitzScreenGridButton[,] blitzScreenGridButtons = new BlitzScreenGridButton[4,4];


        BlitzScreenGrid()
        {
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
                {
                    new BlitzScreenGridButton(i,j,0,0,);
                }
        }

        foreach(BlitzScreenGridButton blitzScreenGridButton in blitzScreenGridButtons){do something}
    }*/
}
