namespace WordBlitz.Screens.BlitzScreen
{
    public class SwipeLogic
    {
#nullable enable    
        public static Tuple<int, int>? GetPosition(object sender, PanUpdatedEventArgs args) //returns (grid row , grid column) if hovered over another button
        {
            var label = (Label)sender;//its a button for now
            var boardGrid = (Grid)label.Parent;


            //notes:
            //margin does not affect coordinates system
            //padding does affect it. Make sure padding is uniform.  note 3/4 is magic number for padding, padding increases the bounds size by its value *2
            //3/8 is magic number for rowspacing, row spacing increases the bounds size by its value set.
            //scale inversely affects swipe sensitivity for some reason.
            //make sure row spacing = column spacing

            double GRID_CELL_SIZE = ( boardGrid.Height + boardGrid.RowSpacing - (boardGrid.Padding.Right / 2) ) / (double) 4;
            //double distanceLabelBoundsToCellBounds = ( GRID_CELL_SIZE - label.Bounds.Width ) / 2;

            double approx_X = label.Bounds.Center.X + (boardGrid.ColumnSpacing * 0.5) - boardGrid.Padding.Right + args.TotalX * label.Scale;
            double approx_Y = label.Bounds.Center.Y + (boardGrid.RowSpacing * 0.5) - boardGrid.Padding.Bottom + args.TotalY * label.Scale;
            // args total X( relative movement since swipe), label X(top left of grid cell), boardGrid X gives topleft of entire grid , label bounds.wid
            /*Console.WriteLine($"margin is {}");*/
            /*Console.WriteLine($"labels Y {label.Y}, X {label.X},{boardGrid.X} , {boardGrid.Y}");
            Console.WriteLine($"bounds -> {label.Bounds} , grid cell height -> {GRID_BOX_SIZE}");*/

            Console.WriteLine($"x actual center{label.Bounds.Center.X}, y actual center = {label.Bounds.Center.Y} "); 
            Console.WriteLine($"approx x {approx_X},approx y {approx_Y}");
            //Console.WriteLine($"padding {boardGrid.Padding.Right}, {"yolo"}");
            //Console.WriteLine($"label X {label.X}");
            
            /*Console.WriteLine($"gridwith - label * 4 : {boardGrid.Bounds.Width-label.Bounds.Width / hitboxProportion}");*/  //<constant

            double DECIMAL_FRACTION_BOX_RADIUS = 0.75;

            if ((approx_X < 0) || (approx_Y < 0)) { return null; }

            int getCharacteristicOffset(double offset)//imagine resolving all positions relative to the top left square with tessellation
            {
                int spaces = 0;
                while (offset > GRID_CELL_SIZE) { offset -= GRID_CELL_SIZE; spaces++; }
                return spaces;
            }

            int currentRow = (int)getCharacteristicOffset(approx_Y);
            int currentCol = (int)getCharacteristicOffset(approx_X);
            /*Console.WriteLine($"current row is {currentRow},current col is {currentCol}");*/

            Tuple<int, int> output = new Tuple<int, int>(currentRow, currentCol);


            double X = approx_X - currentCol * GRID_CELL_SIZE;
            double Y = approx_Y - currentRow * GRID_CELL_SIZE;
            Console.WriteLine($"x is {X}, y is {Y}");


            X = (X > GRID_CELL_SIZE / 2) ? GRID_CELL_SIZE / 2 - X : X - GRID_CELL_SIZE / 2;
            Y = (Y > GRID_CELL_SIZE / 2) ? GRID_CELL_SIZE / 2 - Y : Y - GRID_CELL_SIZE / 2;

            /*Console.WriteLine($"c x offset is {X}, c y offset is {Y}");*/

            bool isGridCellWithinBounds = ((currentRow >= 0 && currentRow < 4) && (currentCol >= 0 && currentCol < 4));

            //logic for circle hitbox
            bool isCoordinatesFitInShape = (Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) ) < (GRID_CELL_SIZE / 2 * DECIMAL_FRACTION_BOX_RADIUS));

            //submit result
            if (isGridCellWithinBounds && isCoordinatesFitInShape) { return output; } else { return null; }
        }
    }
}
