using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBlitz.tools;

namespace WordBlitz.Screens.BlitzScreen
{
    public class BlitzScreenGridButton : Button
    {
        readonly short rowPosition;
        readonly short columnPosition;
        readonly string buttonDisplayLetterValue;

        bool isNotBeenSelected = true;
        bool isInRange = true;
        bool isDisabled = false;
        public bool isEnabled { get { return !isDisabled; } set { isDisabled = !value; } }

        BlitzScreenGridButton[,] parent;

        //constructor
        public BlitzScreenGridButton(short rowParam , short colParam , string letterParam, ref BlitzScreenGridButton[,] parentParam)
        {
            rowPosition = rowParam;
            columnPosition = colParam;
            buttonDisplayLetterValue = letterParam;
            parent = parentParam;
            this.FontSize = 40;
            this.Text = buttonDisplayLetterValue;
            this.Pressed += (sender, args) => updateAllBlitzScreenGridButtons( rowPosition, columnPosition, ref BlitzScreenGrid.linkedButtons);
        }

        //track button activated
        public void selectThisBlitzScreenGridButton()
        {
            isNotBeenSelected = false;
            updateThisBlitzScreenGridButton(rowPosition, columnPosition);
        }

        //track button unactivated
        public void unselectThisBlitzScreenGridButton()//currently unused TODO make backtracking possible
        {
            isNotBeenSelected = true;
            updateThisBlitzScreenGridButton(rowPosition, columnPosition);
        }

        //update button status
        public void updateThisBlitzScreenGridButton(short rowPointer, short colPointer)
        {
            isInRange = (Math.Abs(rowPointer - rowPosition) <= 1) &&
                        (Math.Abs(colPointer - columnPosition) <= 1) ?
                        true : false;
            isEnabled = (isNotBeenSelected && isInRange) ? true : false;
            this.BackgroundColor = isEnabled ? Colors.Navy : Colors.Black;
            this.IsEnabled = isEnabled ? true : false;
        }

        //update all buttons
        public void updateAllBlitzScreenGridButtons(short rowPointer, short colPointer , ref BlitzScreenGridButton[,] linkedButtons)
        {
            linkedButtons[rowPointer, colPointer].selectThisBlitzScreenGridButton();
            for (short i = 0; i < 4; i++) for (short j = 0; j < 4; j++)
                {
                    linkedButtons[i, j].updateThisBlitzScreenGridButton(rowPointer, colPointer);
                }
        }

    }
    /*public class BlitzScreenGridButton : Button
    {
        short rowPosition;
        short columnPosition;
        short rollOrientation;
        string buttonDisplayLetterValue;

        Grid parentGrid;

        bool isNotBeenSelected = true;
        bool isInRange = true;
        bool isDisabled = false;
        public bool isEnabled { get { return !isDisabled; } set { isDisabled = !value; } }


        //button constructor
        public BlitzScreenGridButton(short rowPositionParam,short columnPositionParam,short diceShuffleParam, short diceRollOrientationParam , ref Grid parentGridParam)
        {
            rowPosition = rowPositionParam;
            columnPosition = columnPositionParam;
            rollOrientation = diceRollOrientationParam;
            buttonDisplayLetterValue = Config.currentDice[ diceShuffleParam ][ diceRollOrientationParam ];

            parentGrid = parentGridParam;

            Button button = new()
            {
                BackgroundColor = Colors.Black,
                FontSize = 40,
            };

            button.Pressed += (object sender, EventArgs e) =>
            {
                selectThisBlitzScreenGridButton();
                updateAllBlitzScreenGridButtons(rollOrientation, columnPosition);
            };

            parentGrid.Add(button,rowPosition,columnPosition);
            Console.WriteLine($"{rowPosition} = rowPosition\n" +
                                 $"{columnPosition} = columnPosition\n" +
                                 $"{rollOrientation} = rollOrientation\n" +
                                 $"{buttonDisplayLetterValue} ***************** buttonDisplayLetterValue;");
            Console.WriteLine("this line has been reached");
        }

        //button activated
        public void selectThisBlitzScreenGridButton()
        {
            isNotBeenSelected = false;
            *//*updateAllBlitzScreenGridButtons(rowPosition,columnPosition);*//*
        }

        //button unactivated
        public void unselectThisBlitzScreenGridButton()//currently unused TODO make backtracking possible
        {
            isNotBeenSelected = true;
            *//*updateAllBlitzScreenGridButtons(rollOrientation,columnPosition);*//*
        }

        //update button status
        public void updateThisBlitzScreenGridButton(short currentSelectedRowLocation, short currentSelectedColumnLocation)
        {
            isInRange = (Math.Abs(currentSelectedColumnLocation - columnPosition) <= 1)||
                        (Math.Abs(currentSelectedRowLocation    - rowPosition)    <= 1) ?
                        true : false;
            isEnabled = (isNotBeenSelected && isInRange) ? true : false;
            this.BackgroundColor = isEnabled ? Colors.Navy : Colors.Black ;
        }

        //
*//*        public void updateAllBlitzScreenGridButtons(short currentSelectedRowLocation, short currentSelectedColumnLocation)
        {
            foreach (BlitzScreenGridButton[] rowcolleagues in buttonColleagues) 
            {
                foreach (BlitzScreenGridButton colleague in rowcolleagues)
                {
                    colleague.updateThisBlitzScreenGridButton(currentSelectedRowLocation, currentSelectedColumnLocation);
                }
            }
        }*//*

    }*/
}
