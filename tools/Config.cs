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
