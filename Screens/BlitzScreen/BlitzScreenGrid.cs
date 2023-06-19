using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBlitz.tools;

namespace WordBlitz.Screens.BlitzScreen
{
    public static class BlitzScreenGrid
    {
        static internal void InitialiseBoard(Grid board)
        {
            int[] diceShuffleArray = Enumerable.Range(0, 16).OrderBy(lambda => Global.random.Next()).ToArray();
            int[] diceOrientationArray = new int[16].Select(lambda => Global.random.Next() % 6).ToArray();
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
            {
                Button button = new()
                {
                    BackgroundColor = Colors.Navy,
                    FontSize = 40,
                    Text = Dice.dice[diceShuffleArray[i * 4 + j]][diceOrientationArray[i * 4 + j]]
                };
                button.Pressed += OnButtonPressed;
                board.Add(button, i, j);
            }
        }
        private static void OnButtonPressed(object sender, EventArgs e)
        {
            Button element = (Button)sender;
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
