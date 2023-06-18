using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBlitz.tools;

namespace WordBlitz.Screens.BlitzScreen
{
    public class BlitzScreenGrid
    {
        private static readonly int[] diceShuffleArray = Enumerable.Range(0, 16).OrderBy(lambda => Global.random.Next()).ToArray();
        private static readonly int[] diceOrientationArray = new int[16].Select(lambda => Global.random.Next() % 6).ToArray();
        static internal void InitialiseBoard(Grid board)
        {
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
            {
                Button button = new()
                {
                    BackgroundColor = Colors.Navy,
                    FontSize = 40,
                    Text = Global.currentDice[diceShuffleArray[i * 4 + j]][diceOrientationArray[i * 4 + j]]
                };
                button.Pressed += OnButtonPressed;
                button.Released += OnButtonReleased;
                board.Add(button, i, j);
            }
        }
        private static void OnButtonPressed(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.IsEnabled)
            {
                Grid board = (Grid) button.Parent;
                Global.selectedWord += button.Text;
                button.BackgroundColor = Colors.Black;
                foreach (Button child in board.Children) if (child.BackgroundColor != Colors.Black) child.IsEnabled = true;
                foreach (Button child in board.Children.Where(c =>
                    board.GetRow(c) < board.GetRow(button) - 1 ||
                    board.GetRow(c) > board.GetRow(button) + 1 ||
                    board.GetColumn(c) < board.GetColumn(button) - 1 ||
                    board.GetColumn(c) > board.GetColumn(button) + 1
                )) child.IsEnabled = false;
            }
        }

        private static void OnButtonReleased(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (Global.selectedWord != "") button.IsEnabled = false;
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
