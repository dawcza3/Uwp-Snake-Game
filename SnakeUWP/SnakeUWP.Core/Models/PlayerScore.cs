using GalaSoft.MvvmLight;

namespace SnakeUWP.Core.Models
{
    public class PlayerScore : ViewModelBase
    {
        public int Score { get; set; }
        public string Name { get; set; }
        public int Place { get; set; }

        public PlayerScore(string name,int score,int place)
        {
            Score = score;
            Name = name;
            Place = place;
        }
    }
}