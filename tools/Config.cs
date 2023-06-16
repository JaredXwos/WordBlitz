using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz.tools
{
    public static class Config
    {
        public static string dictionaryConfig = Preferences.Default.Get("dictionaryConfig","CSW22.txt");
        public static string diceTypeConfig = Preferences.Default.Get("diceTypeConfig","DiceModern.txt");
        public static string backgroundConfig = Preferences.Default.Get("backgroundConfig","Zen");
        public static int    blitzTimeConfig = Int32.Parse(Preferences.Default.Get("blitzTimeConfig","180"));


        public static Random random = new(Guid.NewGuid().GetHashCode());

        public static HashSet<string> currentDict = new();
        public static string[][] currentDice = new string[16][];
        public static List<string> submittedWords = new();
    }
}
