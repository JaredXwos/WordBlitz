using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordBlitz.tools
{
    public class BackgroundsMapping
    {
        private Dictionary<string,string> backgroundToFilenameDict = new Dictionary<string, string>()
        {
            {"Zen"                      , "wooden_study.png"        },
            {"NOT Zen"                  , ""                        },
            {"Darkmode"                 , ""                        },
            {"Anime"                    , ""                        },
            {"AHHH my eyes"             , ""                        },
            {"i dont want a background" , ""                        },
            {"SuRpRiSe"                 , ""                        },
            {"Gigachads"                , ""                        },
            {"Scrabble"                 , ""                        },
            //empty string will represent background file does not exist (yet)
            //also update settings.xaml if there are changes to the key value of this dict
        };

        public string getBackgroundFilename(string selectedBackgroundSetting)
        {
            Console.WriteLine($"{backgroundToFilenameDict[selectedBackgroundSetting]} has been SUMMONNNNED, this message is brought to you by background mappings" );
            return backgroundToFilenameDict[selectedBackgroundSetting];
        }
    }
}
