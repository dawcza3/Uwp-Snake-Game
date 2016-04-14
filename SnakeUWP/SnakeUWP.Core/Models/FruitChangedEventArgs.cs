using System;

namespace SnakeUWP.Core.Models
{
    public class FruitChangedEventArgs:EventArgs
    {
        public Fruit FruitUpdated { get; private set; }
        public bool IsEaten { get; private set; }

        public FruitChangedEventArgs(Fruit fruitUpdated, bool isEaten)
        {
            FruitUpdated = fruitUpdated;
            IsEaten = isEaten;
        }
    }
}