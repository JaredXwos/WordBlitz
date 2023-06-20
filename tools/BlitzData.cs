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

        public static SortedSet<string>  GetList => submittedWordsList;

        public static bool SubmitWord(string word) 
        {
            bool isWordUniqueSubmission = submittedWordsList.Add(word); //adds word to list, while also checking if successful
            return isWordUniqueSubmission;
        }

        public static bool RemoveWord(string word)
        {
            bool isWordSuccessfullyRemoved = submittedWordsList.Remove(word);
            return isWordSuccessfullyRemoved;
        }

        public static SortedSet<string> RetrieveAndReset() 
        {
            SortedSet<string> returnList = submittedWordsList;
            submittedWordsList.Clear();
            return returnList;
        }

    }
}
