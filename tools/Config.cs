﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz.tools
{
    public static class Config
    {
        public static string dictionaryConfig  = Preferences.Default.Get("dictionaryConfig","CSW22.txt");
        public static string diceTypeConfig    = Preferences.Default.Get("diceTypeConfig","DiceModern.txt");
        public static string backgroundConfig  = Preferences.Default.Get("backgroundConfig","Zen");
        public static int    blitzTimeConfig   = Preferences.Default.Get("blitzTimeConfig",180);
        public static int    tileSelectionMode = Preferences.Default.Get("TileSelectionMode", 2); //see AllEnum.cs , TileSelectionMode
        public static string pointsConfig = Preferences.Default.Get("pointsConfig",
            "3 1 1\n" +
            "4 1 1\n" +
            "5 2 1\n" +
            "6 3 2\n" +
            "7 5 3\n" +
            "8 7 4"
        );
    }

    public static class Global
    {
        public readonly static Random random = new(Guid.NewGuid().GetHashCode());
        public static Dictionary<int, Tuple<int, int>> points
        {
            get
            {
                string[][] data = Config.pointsConfig.Split('\n').Select(x => x.Split(' ').ToArray()).ToArray();
                Dictionary<int, Tuple<int, int>> _number = new();
                foreach (string[] row in data)
                {
                    _number[Int32.Parse(row[0])] = new Tuple<int, int>(Int32.Parse(row[1]), Int32.Parse(row[2]));
                }
                return _number;
            }
            set
            {
                List<string> list = new();
                foreach (KeyValuePair<int, Tuple<int, int>> pair in value)
                {
                    string[] arr = { pair.Key.ToString(), pair.Value.Item1.ToString(), pair.Value.Item2.ToString() };
                    list.Add(string.Join(" ", arr));
                }
                Preferences.Default.Set("pointsConfig", string.Join('\n', list));
            }
        }
    }
    public static class Submit
    {
        private static Stack<Tuple<int, int>> pos = new();  //Tuple specifically stores an immutable pair
        private static Stack<string> word = new();
        private static List<Tuple<string, int>> list = new(); //Sorted in points of letters/alphabetical order
        private static Label label;
        public static Tuple<string, int> Word()
        {
            Tuple<string, int> wordtuple = null;
            string lastword = string.Join("", word.Reverse());//its a stack, so reversed
            if (word.Count > 2)
            {
                (int reward, int penalty) = list.Count > 0 && list.Select(x => x.Item1).Contains(lastword) ? new Tuple<int, int>(0, 0) : Global.points[word.Count];
                wordtuple = new(lastword, Dict.dict.Contains(lastword) ? reward : penalty);
                list.Add(wordtuple);
                word.Clear(); pos.Clear(); //For each Clear/Push/Pop, word and pos must be done together
            }
            label.Text = lastword;
            return wordtuple ?? new Tuple<string, int>("", 0);
        }
        public static List<Tuple<string, int>> All()
        {
            List<Tuple<string, int>> returnlist = new(list);
            list.Clear();
            return returnlist;
        }
        public static void Letter(string letter, Tuple<int, int> position)
        {
            if (pos.Contains(position)) while (pos.Peek().ToString() != position.ToString()) { pos.Pop(); word.Pop(); } //Keep popping until you're at that last position
            else
            { //letter yet to be pressed
                if (pos.Count == 0) { word.Push(letter); pos.Push(position); return; } //First letter of word
                (int lasti, int lastj) = pos.Peek();
                (int i, int j) = position;
                if (Math.Abs(lasti - i) <= 1 && Math.Abs(lastj - j) <= 1) { word.Push(letter); pos.Push(position); }
            }
        }
        public static int TotalUp(List<Tuple<string, int>> wordlist = null) { return (wordlist ?? list).Select(x => x.Item2).Sum(); }
        public static List<Tuple<string, int>> Getlist() { return list; } //Debug purposes only
        public static List<string> Getword() { return word.ToList(); } //Debug purposes only
        public static void Bind(Label newlabel) { label = newlabel; }
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

    public static class SubmittedList
    {
        private static List<Tuple<string, int>> _list = new();
        public static List<Tuple<string, int>> list { get { _list = Submit.All(); return _list; } }
        private static List<Tuple<string, int>> deleted = new();

        public static bool Toggle(string word)
        {
            List<Tuple<string, int>> source = deleted, destination = _list;
            if (_list.Select(x => x.Item1).Contains(word)) { source = _list; destination = deleted; Console.Write("REMOVE"); }
            Tuple<string, int> tuple = source.Where(x => x.Item1 == word).First();
            source.Remove(tuple); destination.Add(tuple);
            return source == deleted; //returns true for word added, false for word removed
        }
        public static int Total() { return Submit.TotalUp(_list); }
    }
}
