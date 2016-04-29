using System.Threading.Tasks;
using SnakeUWP.Core.Models;

namespace SnakeUWP.Core.Services
{
    public interface ISettings
    {
        void SaveLevelTypeSetting(LevelType levelType);
        void SaveMusicStatusSetting(bool isMusicPlayed);
        bool LoadMusicStatusSetting(bool isMusicPlayed);
        LevelType LoadLevelTypeSetting(LevelType levelType);
        void SaveSettings(LevelType levelType, bool isMusicPlayed);
    }
}