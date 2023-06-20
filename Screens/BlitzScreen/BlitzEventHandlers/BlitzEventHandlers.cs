using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz.Screens.BlitzScreen
{
    //works with the submit class to submit words, acts as the bridge between (swipe and tap events) and (submission logic)
    public static class BlitzEventHandlers 
    {
        static string wordFormed = "0";//TODO IMPLEMENT get word formed by all button presses

        //eventHandlers -----------------------------------------
        public static void submissionHandler() 
        {
            if (isTileLengthLegal(wordFormed)) 
            { Submit.submitToList(); }
        }




        public static void addLetterHandler(object sender)
        {
            var label = (Label)sender;
            var gridBoard = (Grid)label.Parent;
            int i = gridBoard.GetRow(label);
            int j = gridBoard.GetColumn(label);
            Tuple<int,int> position = Tuple.Create(i, j);
            string letter = label.Text;
            Submit.submitLetter(letter , position); 
        }
        public static void addLetterHandler(object sender , int i, int j)
        {
            var label = (Label)sender;
            var gridBoard = (Grid)label.Parent;
            Tuple<int, int> position = Tuple.Create(i, j);
            string letter = label.Text;
            Submit.submitLetter(letter, position);
        }




        public static void processPanInfomationHandler(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Completed) { submissionHandler(); }
#nullable enable
            int[]? rowcolinfo = SwipeLogic.GetGridCoordinates(sender, e);
            if (rowcolinfo != null) { addLetterHandler(sender); }
#nullable disable
        }

        //eventHandlers end -----------------------------------------



        //helper functions
        private static bool isTileLengthLegal(string wordFormed)//TODO make all letters lowercase in resources/raw
        {
            if      (wordFormed.Length > 3)                                 { return true;  }
            else if (wordFormed.Length == 3 && !wordFormed.Contains('Q'))   { return true;  }
            else                                                            { return false; }
        }

        private static void resetGridBoard()
        {
            Console.WriteLine("resetting the board");//TODO IMPLEMENT
        }
    }
}
