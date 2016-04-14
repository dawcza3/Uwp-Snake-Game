using Windows.Foundation;

namespace SnakeUWP.Core.Models
{
    abstract class Snake
    {
        public Direction Direction { get; set; }
        public Point Location { get; set; }

        public SnakeBodyType BodyType { get; set; }

        public Size Size { get; private set; }

        public Rect Area
        {
            get { return new Rect(Location, Size); }
        }

        protected Snake(Point location, Size size,Direction direction,SnakeBodyType bodyType)
        {
            Location = location;
            Size = size;
            Direction = direction;
            BodyType = bodyType;
        }

        public abstract void Move(Direction direction,bool directionChanged=false);
    }
}
