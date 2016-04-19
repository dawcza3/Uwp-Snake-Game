using Windows.Foundation;
using SnakeUWP.Core.ViewModels;

namespace SnakeUWP.Core.Models
{
    public class SnakeBody
    {
        public static Size PlayAreaSize = new Size(400, 300);
        public static readonly Size PlayerSize = new Size(25, 15);
        public static double Scale = 1;
        const double PixelsToMove = 10;

        public Direction Direction { get; set; }
        public Point Location { get; set; }

        public SnakeBodyType BodyType { get; set; }

        public Size Size { get; private set; }

        public Rect Area
        {
            get { return new Rect(Location, Size); }
        }

        public SnakeBody(Direction direction, SnakeBodyType bodyType)
        {
            Location = new Point(PlayerSize.Width, PlayAreaSize.Height * 3);
            Location = new Point(Location.X, PlayAreaSize.Height / 4);
            Size = PlayerSize;
            Direction = direction;
            BodyType = bodyType;
        }

        public bool Move(Direction direction, bool directionChanged = false)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (Location.X > 0)
                    {
                        Location = new Point(Location.X - PixelsToMove, Location.Y);
                    }
                    else return false;
                    break;
                case Direction.Right:
                    if (Location.X*Scale + Size.Width*Scale <= PlayAreaSize.Width)
                        Location = new Point(Location.X + PixelsToMove, Location.Y);
                    else
                        return false;
                    break;
                case Direction.Up:
                    if ((Location.Y > 0))
                        Location = new Point(Location.X, Location.Y - PixelsToMove);
                    else
                        return false;
                    break;
                default:
                    if (Location.Y*Scale + Size.Height*Scale <= PlayAreaSize.Height)
                        Location = new Point(Location.X, Location.Y + PixelsToMove);
                    else
                        return false;
                    break;
            }
            return true;
        }
    }
}