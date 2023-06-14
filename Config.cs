using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz
{
    public static class Config
    {
        //Config settings
        public static string DictName = "CSW22.txt";
        public static string DiceName = "DiceModern.txt";

        //App cache
        public static Random Random = new(Guid.NewGuid().GetHashCode());    //Designates a RNG
        public static HashSet<string> CurrentDict = new();                  //
        public static string[][] CurrentDice = new string[16][];
        public static HashSet<string> SubmittedWords = new();
    }
}
