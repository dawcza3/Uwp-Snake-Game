using System;
using System.Threading.Tasks;
using Windows.Storage;
using SnakeUWP.Core.Models;
using SnakeUWP.Core.Services;

namespace SnakeUWP.Services
{
    public class Settings : ISettings
    {
        ApplicationDataContainer localSettings =
            ApplicationData.Current.LocalSettings;

        public void SaveLevelTypeSetting(LevelType levelType)
        {
            localSettings.Values["levelType"] = levelType.ToString();
        }
        public void SaveMusicStatusSetting(bool isMusicPlayed)
        {
            localSettings.Values["musicPlayed"] = isMusicPlayed;
        }

        public LevelType LoadLevelTypeSetting(LevelType levelType)
        {
            if (localSettings.Values["levelType"] == null)
            {
                SaveLevelTypeSetting(LevelType.Easy); 
                return LevelType.Easy;
            }
            else
            {
                var s = (string) localSettings.Values["levelType"];
                switch (s)
                {
                    case "Easy":
                        return LevelType.Easy;
                    case "Medium":
                        return LevelType.Medium;
                    default:
                        return LevelType.Hard;
                }
            }
        }

        public bool LoadMusicStatusSetting(bool isMusicPlayed)
        {
            if (localSettings.Values["musicPlayed"] == null)
            {
                SaveMusicStatusSetting(false);
                return false;
            }
            else
            {
              return (bool) localSettings.Values["musicPlayed"];
            }
        }

        public void SaveSettings(LevelType levelType, bool isMusicPlayed)
        {
            SaveLevelTypeSetting(levelType);
            SaveMusicStatusSetting(isMusicPlayed);
        }
    }
}