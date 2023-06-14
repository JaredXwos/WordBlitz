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
<<<<<<< HEAD
        public static string backgroundConfig = "Zen";
=======
        public static string backgroundConfig = "zen";
        public static int blitzTimeConfig = 60;
>>>>>>> 72ad49d8fa4bc75f7c640fe5edbab9002eb98224

        public static Random random = new(Guid.NewGuid().GetHashCode());

        public static HashSet<string> currentDict = new();
        public static string[][] currentDice = new string[16][];
        public static List<string> submittedWords = new();
    }
}
