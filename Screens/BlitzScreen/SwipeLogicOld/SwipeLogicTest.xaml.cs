﻿using System;
using System.Collections.Specialized;
using System.Text;
using WordBlitz.Screens.BlitzScreen.SwipeLogicOld;
using WordBlitz.tools;
using Color = Microsoft.Maui.Graphics.Color;

namespace WordBlitz.Screens.BlitzScreen
{
    public partial class SwipeLogicTest : ContentPage
    {
        public static Button[,] labels = new Button[4,4]; // can be optimised to short
        private static volatile Queue<Tuple<string,Tuple<int,int>>> requestqueue = new();
        private static bool active = false;
        public SwipeLogicTest()
        {
            InitializeComponent();
            /*new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                //Console.WriteLine("HANDLED");
                active = true;
                Tuple<int, int> lastpos = new(-1, -1);
                while (active)
                {
                    if (requestqueue.Count > 0)
                    {
                        //Console.WriteLine("TAKEN");
                        (string requesttext, Tuple<int, int> requestpos) = requestqueue.Dequeue();
                        if (requestpos != lastpos)
                        {
                            lastpos = requestpos;
                            //Console.WriteLine("CALLED");
                            Submit.Letter(requesttext, requestpos);
                        }
                    }
                }
            }).Start();*/


            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
            {
                    Button labelParam = new()
                    {
                        Text = $"c{j},r{i}",
                        BackgroundColor = Colors.Red,
/*                        TextColor = Colors.Transparent,*/
                        HeightRequest = (testGrid.Height),
                        WidthRequest = (testGrid.Width),
                        Scale = 1,
/*                        Opacity = 0.25,*/
                        /*CornerRadius = (int)100,// this is an arbitary large number*/
                    };

                    PanGestureRecognizer panGestureRecognizerParam = new();
                    panGestureRecognizerParam.PanUpdated += (s, e) => PanGestureRecognizerParam_PanUpdated(s,e);

                    labelParam.GestureRecognizers.Add(panGestureRecognizerParam);

                    labels[j,i] = labelParam;

                    testGrid.Add(labelParam,j,i);


                //Console.WriteLine($"grid height is {gridHeight}, actual gridh is {testGrid.Height}");
            }
        }
        private void PanGestureRecognizerParam_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var tappedbuton = (Button)sender;
            var boardgrid = (Grid)tappedbuton.Parent;
            var x = e.TotalX;
            var y = e.TotalY;
            int[] coords = new int[2];
            Console.WriteLine($"hee haw{sender.ToString}, {e.StatusType}, {boardgrid.Height * 0.55} , (x= {x}, y= {y})");

            if (SwipeCoordinatesLogic.GetPosition(sender, e) != null) 
            {
                //Console.WriteLine(string.Join(",", SwipeCoordinatesLogic.GetGridCoordinates(sender, e, boardgrid)));
                (int i , int j) = SwipeCoordinatesLogic.GetPosition(sender, e);

                var button = (Button)sender;
                var gridBoard = (Grid)button.Parent;
                int intialRow = gridBoard.GetRow(button);
                int intialCol = gridBoard.GetColumn(button);

                Console.WriteLine();
                Console.WriteLine($"(coords: {j} {i}, row={intialRow} col={intialCol})");
                /*requestqueue.Enqueue(new Tuple<string, Tuple<int, int>>(tappedbuton.Text, new Tuple<int, int>(coords[0], coords[1])));*/
                //Console.WriteLine(requestqueue.Count.ToString());
            };
        }
    }
}
