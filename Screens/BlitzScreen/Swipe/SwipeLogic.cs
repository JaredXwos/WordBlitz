using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz.Screens.BlitzScreen
{
    public class SwipeLogic
    {
#nullable enable    
        public static Tuple<int, int>? GetPosition(object sender, PanUpdatedEventArgs args) //returns (grid row , grid column) if hovered over another button
        {
            var label = (Label)sender;//its a button for now
            var boardGrid = (Grid)label.Parent;
            double GRID_BOX_SIZE = boardGrid.Height / (double) 4;
            double approx_X = args.TotalX + label.X + GRID_BOX_SIZE / 2;
            double approx_Y = args.TotalY + label.Y + GRID_BOX_SIZE / 2;
            /*Console.WriteLine($"approx x {approx_X},approx y {approx_Y}");*/

            double DECIMAL_FRACTION_BOX_RADIUS = 0.8;

            if ((approx_X < 0) || (approx_Y < 0)) { return null; }

            int getCharacteristicOffset(double offset)//imagine resolving all positions relative to the top left square with tessellation
            {
                int spaces = 0;
                while (offset > GRID_BOX_SIZE) { offset -= GRID_BOX_SIZE; spaces++; }
                return spaces;
            }

            int currentRow = (int)getCharacteristicOffset(approx_Y);
            int currentCol = (int)getCharacteristicOffset(approx_X);
            /*Console.WriteLine($"current row is {currentRow},current col is {currentCol}");*/

            Tuple<int, int> output = new Tuple<int, int>(currentRow, currentCol);


            double X = approx_X - currentCol * GRID_BOX_SIZE;
            double Y = approx_Y - currentRow * GRID_BOX_SIZE;
            /*Console.WriteLine($"x is {X}, y is {Y}");*/


            X = (X > GRID_BOX_SIZE / 2) ? GRID_BOX_SIZE / 2 - X : X - GRID_BOX_SIZE / 2;
            Y = (Y > GRID_BOX_SIZE / 2) ? GRID_BOX_SIZE / 2 - Y : Y - GRID_BOX_SIZE / 2;

            /*Console.WriteLine($"c x offset is {X}, c y offset is {Y}");*/

            bool isGridCellWithinBounds = ((currentRow >= 0 && currentRow < 4) && (currentCol >= 0 && currentCol < 4));

            //logic for circle hitbox
            bool isCoordinatesFitInShape = (Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) ) < (GRID_BOX_SIZE / 2 * DECIMAL_FRACTION_BOX_RADIUS));

            //submit result
            if (isGridCellWithinBounds && isCoordinatesFitInShape) { return output; } else { return null; }
        }
    }
}
