using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz.tools
{
    public static class BlitzData
    {
        private static SortedSet<string> submittedWordsList = new();

        public static SortedSet<string>  getList => submittedWordsList;

        public static bool submitWord(string word) 
        {
            bool isWordUniqueSubmission = submittedWordsList.Add(word); //adds word to list, while also checking if successful
            return isWordUniqueSubmission;
        }

        public static bool removeWord(string word)
        {
            bool isWordSuccessfullyRemoved = submittedWordsList.Remove(word);
            return isWordSuccessfullyRemoved;
        }

        public static void reset(string word) { submittedWordsList = new(); }

    }
}
