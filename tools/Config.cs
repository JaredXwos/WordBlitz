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
        public static int    blitzTimeConfig = Preferences.Default.Get("blitzTimeConfig",180);
    }

    public class Global
    {
        public readonly static Random random = new(Guid.NewGuid().GetHashCode());
        public readonly static Dictloading Dictloader = new();
        public readonly static Diceloading Diceloader = new();

        public static HashSet<string> currentDict = new();
        public static string[][] currentDice = new string[16][];
        public static string selectedWord = "";
        public static List<string> submittedWords = new();
    }
    public class Dictloading
    {
        private Task<bool> task;
        public void Start()
        {
            task = new(() =>
            {
                using var stream = FileSystem.OpenAppPackageFileAsync(Config.dictionaryConfig);
                using var reader = new StreamReader(stream.Result);
                Global.currentDict = new HashSet<string>(reader.ReadToEnd().Split('\n'));
                return true;
            });
            task.Start();
        }
        public void Wait() { task.Wait(); }
    }

    public class Diceloading
    {
        private Task<bool> task;
        public void Start()
        {
            task = new(() =>
            {
                using var stream = FileSystem.OpenAppPackageFileAsync(Config.diceTypeConfig);
                using var reader = new StreamReader(stream.Result);
                Global.currentDice = reader.ReadToEnd().Split('\n').Select(s => s.Split(' ').Select(r => r.Trim()).ToArray()).ToArray();
                return true;
            });
            task.Start();
        }
        public void Wait() { task.Wait(); }
    }
}
