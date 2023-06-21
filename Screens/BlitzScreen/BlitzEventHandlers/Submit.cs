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
            if (tileLettersStack.Count != 0 )
            {
                isSubmissionSuccessful = BlitzData.SubmitWord(FormTheWord);
            }
            clearLetterTileStack();
            return isSubmissionSuccessful;
        }

        public static void clearLetterTileStack()
        {
            tileLettersStack.Clear(); tilePositions.Clear();
        }

        public static string FormTheWord { get { return string.Join("", tileLettersStack.Reverse()); ; } }//its a stack, so reversed

        private static bool isLastTileInRange(int lasti ,int lastj , int i , int j)
        {
            return (Math.Abs(lasti - i) <= 1 && Math.Abs(lastj - j) <= 1);
        }

        private static bool isSameTile(int lasti, int lastj, int i, int j)
        {
            return ((lasti == i) && (lastj == j));
        }
        private static bool isPreviousTile(int secondlasti, int secondlastj, int i, int j)
        {
            return ((secondlasti == i) && (secondlastj == j));
        }


        public static void submitLetter(string letter, Tuple<int, int> position)
        {
            if (tilePositions.Count == 0) { tileLettersStack.Push(letter); tilePositions.Push(position); return; } //if empty, set it
            int i = position.Item1;
            int j = position.Item2;
            if (tilePositions.Count >= 2) //backtrack to last letter if selected
            {
                int secondlasti = tilePositions.ElementAt(1).Item1;
                int secondlastj = tilePositions.ElementAt(1).Item2;
                if (isPreviousTile(secondlasti, secondlastj, i, j))
                {
                    tilePositions.Pop(); tileLettersStack.Pop();//For each Clear/Push/Pop, word and pos must be done together
                    return;
                }
            }
            int lasti = tilePositions.ElementAt(0).Item1;
            int lastj = tilePositions.ElementAt(0).Item2;
            if (isLastTileInRange(lasti, lastj, i, j) && !isSameTile(lasti, lastj, i, j))//checks if tile is valid selection
            {
                tileLettersStack.Push(letter); tilePositions.Push(position);
                return;
            }
        }
    }
}
