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
        private int[] diceShuffleArray = Enumerable.Range(0, 16).OrderBy(lambda => Config.random.Next()).ToArray() /*Enumerable.Repeat(0, 16).ToArray();*/ ;
        private int[] diceOrientationArray = new int[16].Select(lambda => Config.random.Next() % 6).ToArray()/*Enumerable.Repeat(0, 16).ToArray();*/ ;

        Grid boardGrid;
        short rowPointer;
        short colPointer;
        public static BlitzScreenGridButton[,] linkedButtons = new BlitzScreenGridButton[4, 4];
        public BlitzScreenGrid(ref Grid boardGridParam)
        {
            boardGrid = boardGridParam;

            for (short i = 0; i < 4; i++) for (short j = 0; j < 4; j++)
                {
                    string text = Config.currentDice[diceShuffleArray[i * 4 + j]][diceOrientationArray[i * 4 + j]];
                    linkedButtons[i, j] = new BlitzScreenGridButton(i , j , text , ref linkedButtons);

                    boardGrid.Add(linkedButtons[i,j], i, j);
                }

                    
        }

        public void clickGridButton (short row, short col)
        {
            linkedButtons[row, col].selectThisBlitzScreenGridButton();
            for (short i = 0; i < 4; i++) for (short j = 0; j < 4; j++)
                {
                    linkedButtons[i,j].updateThisBlitzScreenGridButton(row, col); 
                }
            
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
