using System;

namespace SnakeUWP.Core.Models
{
    public class SnakeChangedEventArgs : EventArgs
    {
        public SnakeBody SnakeUpdated { get; private set; }
        public bool Killed { get; private set; }

        public SnakeChangedEventArgs(SnakeBody snakeUpdated, bool killed)
        {
            SnakeUpdated = snakeUpdated;
            Killed = killed;
        }
    }
}
