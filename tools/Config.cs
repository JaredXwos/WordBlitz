using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz.tools
{
    public static class Config
    {
        public static string dictionaryConfig = "CSW22.txt";
        public static string diceTypeConfig = "DiceModern.txt";
        public static string backgroundConfig = "zen";

        public static Random random = new(Guid.NewGuid().GetHashCode());

        public static HashSet<string> currentDict = new();
        public static string[][] currentDice = new string[16][];
        public static List<string> submittedWords = new();
    }
}
