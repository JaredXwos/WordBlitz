using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz.Screens.BlitzScreen.SwipeLogic
{
    public static class SwipeCoordinatesLogic

    {
        public static int[]? GetGridCoordinates(object sender, PanUpdatedEventArgs args, Grid boardGrid) //returns (grid row , grid column) if hovered over another button
        {
            var label = (Button)sender;//its a button for now
            double initial_X = label.X;
            double initial_Y = label.Y;
            double relative_X = args.TotalX;
            double relative_Y = args.TotalY;
            double GRID_BOX_SIZE = boardGrid.Height / (double)4;
            double DECIMAL_FRACTION_BOX_RADIUS = 0.75;

            /*int startRow = label.Text.ToList()[0]; // if putting info inside 
            int startCol = label.Text.ToList()[1];*/

            double[] getCharacteristicOffset (double offset)//imagine resolving all positions relative to the top left square with tessellation
            {
                int spaces = 0;
                double isNotNegative = 1;
                if (offset < 0) { isNotNegative = -1;}
                double absoffset = offset * isNotNegative;
                while (absoffset >= GRID_BOX_SIZE) { absoffset -= GRID_BOX_SIZE; spaces++; }
                double[] loopOutput = new double[] { spaces * isNotNegative, absoffset };
                return loopOutput;
            }

            int startRow = (int)getCharacteristicOffset(initial_X)[0];
            int startCol = (int)getCharacteristicOffset(initial_Y)[0];
            int AdjustedRows = (int)getCharacteristicOffset(relative_X)[0];
            int AdjustedCols = (int)getCharacteristicOffset(relative_Y)[0];
            int finalRow = startRow + AdjustedRows;
            int finalCol = startCol + AdjustedCols;
            int[] output = new int[] { finalRow, finalCol };

            double X = (int)getCharacteristicOffset(relative_X)[1];
            double Y = (int)getCharacteristicOffset(relative_Y)[1];

            bool isGridCellWithinBounds = (finalRow >= 0 && finalRow <4 && finalCol >= 0 && finalRow<4);
            bool isCoordinatesFitInShape = (Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)) < GRID_BOX_SIZE * DECIMAL_FRACTION_BOX_RADIUS);

            //circle logic
            if (isGridCellWithinBounds && isCoordinatesFitInShape){return output;} else{return null;}

        }
    }
}
