using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordBlitz.tools;

namespace WordBlitz.Screens.BlitzScreen
{
    public static class Submit
    {
        private static volatile Stack<Tuple<int, int>>          tilePositions = new();  //Tuple specifically stores an immutable pair
        private static          Stack<string>                tileLettersStack = new(); //previously named word

        public static bool submitToList()//previously named Word()
        {
            bool isSubmissionSuccessful = false;
            if (tileLettersStack.Count != 0)
            {
                isSubmissionSuccessful = BlitzData.SubmitWord(FormTheWord);
                if (isSubmissionSuccessful) { Console.WriteLine( $"SUBMITTED WORD {FormTheWord}"); }
                clearLetterTileStack();
                return isSubmissionSuccessful;
            }
            else
            {
                clearLetterTileStack();
                return false;
            }
        }

        public static void clearLetterTileStack()
        {
            tileLettersStack.Clear(); tilePositions.Clear();
        }

        public static string FormTheWord { get { return string.Join("", tileLettersStack.Reverse()); ; } }//its a stack, so reversed


        public static void submitLetter(string letter, Tuple<int, int> position)
        {
            if (tilePositions.Count == 0) { tileLettersStack.Push(letter); tilePositions.Push(position); Console.WriteLine($"set first letter{letter}"); return;  } //if empty, set it
            (int i, int j) = position;

            if (tilePositions.Count >= 2) //to allow for backtracking
            {
                Console.WriteLine("sumitletter is being called");
                int secondlasti = tilePositions.ElementAt(1).Item1;
                int secondlastj = tilePositions.ElementAt(1).Item2;
                if ((secondlasti == i) && (secondlastj == j)/*checks if previous tile is selected*/)
                {
                    tilePositions.Pop(); tileLettersStack.Pop();//For each Clear/Push/Pop, word and pos must be done together
                    Console.WriteLine("returned to previous letter");
                    return;
                }
            }
            if (tilePositions.Contains(position)) { return; } //repeat presses on same button is ignored
            int lasti = tilePositions.ElementAt(0).Item1;
            int lastj = tilePositions.ElementAt(0).Item2;
            if ((Math.Abs(lasti - i) <= 1 && Math.Abs(lastj - j) <= 1) &&/*check if tiles are in range and*/
                !((lasti == i) && (lastj == j)))  /*checks if the same tile submitted*/  //checks if tile is valid selection
            {
                tileLettersStack.Push(letter); tilePositions.Push(position);
                Console.WriteLine($"submitted letter {letter}");
                return;
            }
        }
    }
}
