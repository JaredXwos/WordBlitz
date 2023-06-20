using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBlitz.tools;
using static System.Net.Mime.MediaTypeNames;

namespace WordBlitz.Screens.BlitzScreen
{
    public static class BoardInitialiser

    {
        static internal void InitialiseBoard(Grid board)
        {
            Grid boardGrid = board;
            int[] diceShuffleArray = Enumerable.Range(0, 16).OrderBy(lambda => Global.random.Next()).ToArray();
            int[] diceOrientationArray = new int[16].Select(lambda => Global.random.Next() % 6).ToArray();

            Console.WriteLine($"{boardGrid.HeightRequest} height is this");

            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
            {
                    string ijText = Dice.dice[diceShuffleArray[i * 4 + j]][diceOrientationArray[i * 4 + j]];
                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) =>
                    {
                        OnGridButtonTapped(s, e);
                        Console.WriteLine("IVE BEEN TAPPED");
                    };

                    //insert constructor for images here

                    Label labelButton = new Label()//will become invisible
                    {
                        Text = ijText,
                        BackgroundColor = Colors.Red,
                        TextColor = Colors.Transparent,
                        HeightRequest = (boardGrid.HeightRequest / 4),
                        WidthRequest = (boardGrid.WidthRequest / 4),
                        Scale = 0.5,
                        Opacity = 0.75,
                        GestureRecognizers = { tapGestureRecognizer }
                    };

                    boardGrid.Add(labelButton,i,j);
            }
        }

        private static void OnGridButtonPanned(object sender, PanUpdatedEventArgs e)
        {
            BlitzEventHandlers.processPanInfomationHandler(sender, e);
        }

        private static void OnGridButtonTapped(object sender, TappedEventArgs e)
        {
            var label = (Label)sender;
            label.IsEnabled = false;
            BlitzEventHandlers.addLetterHandler(sender);
            label.IsEnabled = true;
        }

        private static void OnSubmitButtonTapped(object sender, TappedEventArgs e)
        {
            var label = (Label)sender;
            label.IsEnabled = false;
            BlitzEventHandlers.submissionHandler();
            label.IsEnabled = true;
            Console.WriteLine("SUBMIT");
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
