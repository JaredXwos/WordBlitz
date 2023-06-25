using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WordBlitz.Screens.BlitzScreen;

namespace WordBlitz.tools
{
    public static class Config
    {
        public static string dictionaryConfig  = Preferences.Default.Get("dictionaryConfig","CSW22.txt");
        public static string diceTypeConfig    = Preferences.Default.Get("diceTypeConfig","DiceModern.txt");
        public static string backgroundConfig  = Preferences.Default.Get("backgroundConfig","Zen");
        public static int    blitzTimeConfig   = Preferences.Default.Get("blitzTimeConfig",180);
        public static int    tileSelectionMode = Preferences.Default.Get("TileSelectionMode", 2); //see AllEnum.cs , TileSelectionMode
        public static string gamemodeConfig    = Preferences.Default.Get("gameModeConfig", "Realistic");
        public static string pointsConfig      = Preferences.Default.Get("pointsConfig",
            "3 1 -1\n" +
            "4 1 -1\n" +
            "5 2 -1\n" +
            "6 3 -2\n" +
            "7 5 -3\n" +
            "8 7 -4"
        );
    }

    public static class Global
    {
        public readonly static Random random = new(Guid.NewGuid().GetHashCode());
        public static void Init()
        {
            Points.Start();
            Dict.Start();
            Dice.Start();
        }
    }

    public static class Load
    {
        private static bool loading = false;
        public static bool Toggle() { loading = !loading; return loading; }
        public static bool Get() => loading;
    }

    public static class Settings
    {
        private readonly static ContentPage page = new SettingsScreen();
        public static ContentPage Update()
        {
            IElement[] elements = ((StackLayout)page.Content).Children.ToArray();

            ((Picker)((StackLayout)elements[0]).Children.ToArray()[1]).SelectedItem = Config.dictionaryConfig;
            ((Picker)((StackLayout)elements[1]).Children.ToArray()[1]).SelectedItem = Config.diceTypeConfig;
            ((Picker)((StackLayout)elements[2]).Children.ToArray()[1]).SelectedItem = Config.backgroundConfig;
            ((Picker)((StackLayout)elements[3]).Children.ToArray()[1]).SelectedIndex = Config.tileSelectionMode;
            ((Picker)((StackLayout)elements[4]).Children.ToArray()[1]).SelectedItem = Config.gamemodeConfig;
            ((Entry)((StackLayout)((StackLayout)elements[5]).Children.ToArray()[0])[1]).Text = Config.blitzTimeConfig.ToString();
            ((Slider)((StackLayout)elements[5]).Children.ToArray()[1]).Value = Config.blitzTimeConfig;
            return page;
        }
        public static ContentPage Get() => page;
    }

    public static class Blitz
    {
        private volatile static ContentPage page = new BlitzScreen();
        public static ContentPage Update()
        {
            Button button = new();
            IElement[] elements = 
                ((Grid)((Grid)((Frame)((VerticalStackLayout)((Grid)page.Content).
                Children[1]).Children[0]).Children[0]).Children[0]).Children.Where(x=>x.GetType()==button.GetType()).ToArray();

            int[] diceShuffleArray = Enumerable.Range(0, 16).OrderBy(lambda => Global.random.Next()).ToArray();
            int[] diceOrientationArray = new int[16].Select(lambda => Global.random.Next() % 6).ToArray();
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++)
            {
                boardInitialiser.gridLayout[j, i] = Dice.dice[diceShuffleArray[j * 4 + i]][diceOrientationArray[j * 4 + i]];
                ((Button)elements[j * 4 + i]).Text = boardInitialiser.gridLayout[j, i];
            }
            return page;
        }
        public static ContentPage Get() => page;
    }
    public static class Points
    {
        private static Dictionary<int, int[]> _points = new();
        public static void Start()
        {
            string[][] data = Config.pointsConfig.Split('\n').Select(x => x.Split(' ').ToArray()).ToArray();
            Dictionary<int, Tuple<int, int>> number = new();
            foreach (string[] row in data)
                _points[Int32.Parse(row[0])] = new int[] { Int32.Parse(row[1]), Int32.Parse(row[2]) };
        }
        public static Dictionary<int, Tuple<int, int>> Get()
        {
            Dictionary<int, Tuple<int, int>> points = new();
            foreach (KeyValuePair<int, int[]> pair in _points) 
                points[pair.Key] = new Tuple<int, int>(pair.Value[0], pair.Value[1]);
            return points;
        }
        public static void Set(int length, string category, int value)
        {
            if (length>2) switch(category)
            {
                case "reward": { _points[length][0] = value;  break; }
                case "penalty": { _points[length][1] =  value; break; }
                default: break;
            }
            List<string> list = new();
            foreach (KeyValuePair<int, int[]> pair in _points)
                list.Add(string.Join(" ", new string[] { pair.Key.ToString(), pair.Value[0].ToString(), pair.Value[1].ToString() }));
            Preferences.Default.Set("pointsConfig", string.Join('\n', list));
        }
    }

    public static class Submit
    {
        private static Stack<Tuple<int, int>> pos = new();  //Tuple specifically stores an immutable pair
        private static Stack<string> word = new();
        private static List<Tuple<string, int>> list = new(); //Sorted in points of letters/alphabetical order
        private static Label label;

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
        public static Tuple<string, int> Word()
        {
            Tuple<string, int> wordtuple = null;
            string lastword = string.Join("", word.Reverse());//its a stack, so reversed
            if (word.Count > 2)
            {
                (int reward, int penalty) = list.Count > 0 && list.Select(x => x.Item1).Contains(lastword) ? new Tuple<int, int>(0, 0) : Points.Get()[word.Count];
                wordtuple = new(lastword, Dict.dict.Contains(lastword) ? reward : penalty);
                list.Add(wordtuple);
                word.Clear(); pos.Clear(); //For each Clear/Push/Pop, word and pos must be done together
            }
            label.Text = lastword;
            if (Config.gamemodeConfig == "Instant") label.Text += " - " + TotalUp().ToString();
            return wordtuple ?? new Tuple<string, int>("", 0);
        }
        public static List<Tuple<string, int>> All()
        {
            List<Tuple<string, int>> returnlist = new(list);
            list.Clear();
            return returnlist;
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
            if (_list.Select(x => x.Item1).Contains(word)) { source = _list; destination = deleted; Console.WriteLine("REMOVE" + word); }
            Tuple<string, int> tuple = source.Where(x => x.Item1 == word).First();
            source.Remove(tuple); destination.Add(tuple);
            return source == deleted; //returns true for word added, false for word removed
        }
        public static int Total() { return Submit.TotalUp(_list); }
    }
}
