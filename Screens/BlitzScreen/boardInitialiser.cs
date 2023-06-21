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

                    Label label = new Label()
                    {
                        Text = /*$"{j},{i}"*/gridLayout[j, i],
                        TextColor = Colors.White,
                        BackgroundColor = Colors.Navy,
                        FontSize = 40,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center,
                        WidthRequest = board.HeightRequest/4,
                        HeightRequest = board.HeightRequest/4,
                        Scale = 1,
                        Opacity = 1,
                    };
                    board.Add(label, j, i);

                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) => { OnButtonPressed(s, e); };
                    PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
                    panGestureRecognizer.PanUpdated += (s, e) => { OnGridButtonPanned(s, e); };

                    Label touchLabel = new Label()  // invisible label
                    {
                        TextColor = Colors.White,
                        BackgroundColor = Colors.Red,
                        FontSize = 40,
                        HorizontalTextAlignment =TextAlignment.Center,
                        VerticalTextAlignment =TextAlignment.Center,
                        WidthRequest = board.HeightRequest/4,
                        HeightRequest = board.HeightRequest/4,
                        Scale = 1,
                        Opacity = 0.05,
                        GestureRecognizers = { panGestureRecognizer },
                    };

                    board.Add(touchLabel, j, i); //important: grid.add() is defined as grid.add(element,COL,row) and not vice versa, so for row=i , col =j its j,i here
            }
        }
        private static void OnButtonPressed(object sender, EventArgs e)
        {
            var element = (Label)sender;
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
                (int i, int j) = position;
                Console.WriteLine($"submitted letter = {gridLayout[j,i]}");
                Console.WriteLine($"(coords: {i} {j}, row={i} col={j}) from boardinitialiser, awaiting uncommenting");
                /*Submit.Letter(gridLayout[j, i], position);*/
/*
                requestqueue.Enqueue(new Tuple<string, Tuple<int, int>>(gridLayout[j,i], position));*/
                //Console.WriteLine(requestqueue.Count.ToString());
            };
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
