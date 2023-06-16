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
        readonly short rowPosition; //shows the row where this button is located
        readonly short columnPosition; //shows the column where this button is located
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
            this.Pressed += (sender, args) => updateAllBlitzScreenGridButtons( rowPosition, columnPosition, ref initialiseBlitzScreenGrid.linkedButtons);
        }

        //track button activated
        public void selectThisBlitzScreenGridButton()
        {
            isNotBeenSelected = false;
            updateThisBlitzScreenGridButton(rowPosition, columnPosition);
            BlitzScreen.selectedword += buttonDisplayLetterValue;
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

}
