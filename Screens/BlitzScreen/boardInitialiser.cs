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
        static internal void InitialiseBoard(Grid board)
        {
            int[] diceShuffleArray = Enumerable.Range(0, 16).OrderBy(lambda => Global.random.Next()).ToArray();
            int[] diceOrientationArray = new int[16].Select(lambda => Global.random.Next() % 6).ToArray();
            string[,] gridLayout = new string[4, 4];
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
            {
                    gridLayout[i, j] = Dice.dice[diceShuffleArray[i * 4 + j]][diceOrientationArray[i * 4 + j]];

                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) =>{ OnButtonPressed(s, e); };
                    /*PanGestureRecognizer panGestureRecognizer = new PanGestureRecognizer();
                    panGestureRecognizer.PanUpdated += (s, e) => { OnButtonPressed(s, e); };*/

                    Label label = new Label()
                    {
                        Text = gridLayout[i, j],
                        TextColor = Colors.White,
                        BackgroundColor = Colors.Navy,
                        FontSize = 40,
                        HorizontalTextAlignment =TextAlignment.Center,
                        VerticalTextAlignment =TextAlignment.Center,
                        WidthRequest = 75,
                        HeightRequest = 75,
                        GestureRecognizers = { tapGestureRecognizer },
                    };

                board.Add(label, j, i);
            }
        }
        private static void OnButtonPressed(object sender, EventArgs e)
        {
            Label element = (Label)sender;
            Grid board = (Grid)element.Parent;
            Submit.Letter(element.Text, new Tuple<int, int> (board.GetRow(element), board.GetColumn(element)));
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
