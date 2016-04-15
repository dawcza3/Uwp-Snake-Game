using GalaSoft.MvvmLight;

namespace SnakeUWP.Core.Models
{
    public class PlayerScore : ViewModelBase
    {
        public int Score { get; set; }
        public string Name { get; set; }

        public PlayerScore(string name,int score)
        {
            Score = score;
            Name = name;
        }
    }
}