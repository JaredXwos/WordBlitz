using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz.Screens.BlitzScreen.SwipeLogicOld
{
    public static class SwipeCoordinatesLogic
    {
#nullable enable    
        public static Tuple<int, int>? GetPosition(object sender, PanUpdatedEventArgs args) //returns (grid row , grid column) if hovered over another button
        {
            var label = (Button)sender;//its a button for now
            var boardGrid = (Grid)label.Parent;
            double GRID_BOX_SIZE = boardGrid.Height / (double)4;
            double approx_X = args.TotalX + label.X;
            double approx_Y = args.TotalY + label.Y;
/*            Console.WriteLine($"initial x is {label.X},intial y is {label.Y}");
            Console.WriteLine($"approx x is {approx_X},approx y is {approx_Y}");*/
            double DECIMAL_FRACTION_BOX_RADIUS = 0.75;

            if ((approx_X < 0) || (approx_Y < 0)) { return null; }

            int getCharacteristicOffset(double offset)//imagine resolving all positions relative to the top left square with tessellation
            {
                int spaces = 0;
                while (offset > GRID_BOX_SIZE) { offset -= GRID_BOX_SIZE; spaces++; }
                return spaces;
            }

            int currentRow = (int)getCharacteristicOffset(approx_Y);
            int currentCol = (int)getCharacteristicOffset(approx_X);

            Tuple<int, int> output = new Tuple<int, int>(currentRow, currentCol);

            double X = (int)getCharacteristicOffset(approx_X);
            double Y = (int)getCharacteristicOffset(approx_Y);

            bool isGridCellWithinBounds = ((currentRow >= 0 && currentRow < 4) && (currentCol >= 0 && currentCol < 4));

            //logic for circle hitbox
            bool isCoordinatesFitInShape = (Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)) < GRID_BOX_SIZE * DECIMAL_FRACTION_BOX_RADIUS);

            //submit result
            if (isGridCellWithinBounds && isCoordinatesFitInShape) { return output; } else { return null; }
        }
    }
}
