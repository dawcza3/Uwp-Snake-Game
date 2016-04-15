﻿using Windows.Foundation;
using SnakeUWP.Core.ViewModels;

namespace SnakeUWP.Core.Models
{
    public class SnakeBody
    {
        private static Size _playAreaSize = new Size(400, 300);
        public static readonly Size PlayerSize = new Size(25, 15);

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
            Location = new Point(PlayerSize.Width, _playAreaSize.Height * 3);
            Location = new Point(Location.X, _playAreaSize.Height / 4);
            Size = PlayerSize;
            Direction = direction;
            BodyType = bodyType;
        }

        public void Move(Direction direction, bool directionChanged = false)
        {
          /*  if (!(Location.X > 0 && Location.X < 400 - PlayerSize.Width) && (Location.Y > 0 && Location.Y < 300 - PlayerSize.Height))
                return;
         */   switch (direction)
            {
                case Direction.Left:
                    if (Location.X > 0)
                    Location = new Point(Location.X - PixelsToMove, Location.Y);
                    break;
                case Direction.Right:
                    if (Location.X  < _playAreaSize.Width-PlayerSize.Width*2)
                    Location = new Point(Location.X + PixelsToMove, Location.Y);
                    break;
                case Direction.Up:
                    if (Location.Y > 0)
                    Location = new Point(Location.X, Location.Y - PixelsToMove);
                    break;
                default:
                    if (Location.Y < _playAreaSize.Height - PlayerSize.Height*6)
                    Location = new Point(Location.X, Location.Y + PixelsToMove);
                    break;
            }
        }
    }
}