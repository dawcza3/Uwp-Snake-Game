using Windows.Foundation;

namespace SnakeUWP.Core.Models
{

    public class Fruit 
    {
        public Point Location { get; protected set; }

        public Size Size { get; private set; }

        public Rect Area => new Rect(Location, Size);

        public static readonly Size FruitSize = new Size(15, 15);

        public FruitType MyFruitType { get; private set; }

        public int Score { get; private set; }

        public Fruit(FruitType fruitType, Point location, int score)
        {
            Location = location;
            Size = FruitSize;
            Score = score;
            MyFruitType = fruitType;
        }

    }
}
