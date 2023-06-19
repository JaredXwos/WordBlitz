using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz.tools
{
    public static class Config
    {
        public static string dictionaryConfig = Preferences.Default.Get("dictionaryConfig","CSW22.txt");
        public static string diceTypeConfig   = Preferences.Default.Get("diceTypeConfig","DiceModern.txt");
        public static string backgroundConfig = Preferences.Default.Get("backgroundConfig","Zen");
        public static int    blitzTimeConfig  = Preferences.Default.Get("blitzTimeConfig",180);
    }

    public static class Global
    {
        public readonly static Random random = new(Guid.NewGuid().GetHashCode());
    }
    public static class Submit
    {
        private static Stack<Tuple<int, int>>  pos  = new();  //Tuple specifically stores an immutable pair
        private static Stack<string>           word = new();
        private static SortedSet<string>       list = new(); //Sorted in number of letters/alphabetical order
        public static string Word() {
            string lastword = string.Join("", word.Reverse());
            if (word.Count != 0) {
                list.Add(lastword); //its a stack, so reversed
                word.Clear(); pos.Clear(); //For each Clear/Push/Pop, word and pos must be done together
            }
            return lastword;
        }
        public static SortedSet<string> All() { SortedSet<string> returnlist = new(list) ; list.Clear();  return returnlist ; }
        public static void Letter(string letter, Tuple<int,int> position)
        {
            if (pos.Contains(position)) while (pos.Peek() != position) { pos.Pop(); word.Pop(); } //Keep popping until you're at that last position
            else{ //letter yet to be pressed
                if(pos.Count==0) { word.Push(letter); pos.Push(position); return; } //First letter of word
                (int lasti, int lastj)  = pos.Peek();
                (int i,     int j)      = position;
                if(Math.Abs(lasti - i) <= 1 && Math.Abs(lastj - j) <= 1) { word.Push(letter); pos.Push(position); }
            }
        }
        public static Tuple<int,int> Lastpos() { return pos.Peek(); } //Forgot if we need this, delete if necessary
        public static List<string> Getlist() { return list.ToList(); } //Debug purposes only
        public static List<string> Getword() { return word.ToList(); } //Debug purposes only
    }

    public static class Dict
    {
        private static Task<bool> task;
        public static HashSet<string> dict = new();
        public static void Start()
        {
            task = new(() =>
            {
                using var stream = FileSystem.OpenAppPackageFileAsync(Config.dictionaryConfig);
                using var reader = new StreamReader(stream.Result);
                dict = new HashSet<string>(reader.ReadToEnd().Split('\n'));
                return true;
            });
            task.Start();
        }
        public static void Wait() { task.Wait(); }
    }

    public static class Dice
    {
        private static Task<bool> task;
        public static string[][] dice = new string[16][];
        public static void Start()
        {
            task = new(() =>
            {
                using var stream = FileSystem.OpenAppPackageFileAsync(Config.diceTypeConfig);
                using var reader = new StreamReader(stream.Result);
                dice = reader.ReadToEnd().Split('\n').Select(s => s.Split(' ').Select(r => r.Trim()).ToArray()).ToArray();
                return true;
            });
            task.Start();
        }
        public static void Wait() { task.Wait(); }
    }
}
