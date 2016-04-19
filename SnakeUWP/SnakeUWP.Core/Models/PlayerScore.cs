using GalaSoft.MvvmLight;

namespace SnakeUWP.Core.Models
{
    public class PlayerScore : ViewModelBase
    {
        private int _score;

        public int Score
        {
            get
            {
                return _score;
            }
            set { Set(ref _score, value); }
        }

        private string _name;
        public string Name { get {return _name;} set { Set(ref _name, value); } }
        private int _place;
        public int Place { get { return _place; } set { Set(ref _place, value); } }

        public PlayerScore(string name,int score,int place)
        {
            Score = score;
            Name = name;
            Place = place;
        }
    }
}