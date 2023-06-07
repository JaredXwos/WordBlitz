using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz
{
    public static class Config
    {
        public static string DictName = "CSW22.txt";
        public static string DiceName = "DiceModern.txt";
        public static Random Random = new(Guid.NewGuid().GetHashCode());

        public static HashSet<string> Lexicon = new();
        public static string[][] CurrentDice = new string[16][];
    }
}
