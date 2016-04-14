using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;

namespace SnakeUWP.Core.Models
{
    internal class SnakeGameModel
    {
        public static readonly Size PlayAreaSize = new Size(400, 300);
        private readonly Random _random = new Random();
        private List<SnakeBody> _snakeBodies = new List<SnakeBody>();
        public List<SnakeBody> SnakeBodies { get { return _snakeBodies; } }
        private Fruit fruit;

        public SnakeGameModel()
        {
            EndGame();
            Score = 0;
        }

        public int Score { get; private set; }

        public bool GameOver { get; private set; }

        public void EndGame()
        {
            GameOver = true;
        }

        public void StartGame()
        {
            GameOver = false;
            SnakeBody snakeBody = new SnakeBody(Direction.Right, SnakeBodyType.SnakeHead);
            _snakeBodies.Add(snakeBody);
            OnSnakeChanged(snakeBody, false);
            NextFruit();
        }

        public void MovePlayer()
        {
            for (int index = _snakeBodies.Count - 1; index > 0; index--)
            {
                _snakeBodies[index].Direction = _snakeBodies[index - 1].Direction;
                _snakeBodies[index].Location=_snakeBodies[index-1].Location;
                OnSnakeChanged(_snakeBodies[index], false);
            }
            _snakeBodies.First().Move(_snakeBodies.First().Direction);
            OnSnakeChanged(_snakeBodies.First(), false);
        }

        public event EventHandler<SnakeChangedEventArgs> SnakeChanged;

        protected void OnSnakeChanged(SnakeBody snakeUpdated, bool killed)
        {
            var shipChanged = SnakeChanged;
            shipChanged?.Invoke(this, new SnakeChangedEventArgs(snakeUpdated, killed));
        }

        public event EventHandler<FruitChangedEventArgs> FruitChanged;

        protected void OnFruitChanged(Fruit fruit, bool isEaten)
        {
            FruitChanged?.Invoke(this, new FruitChangedEventArgs(fruit, isEaten));
        }

        public void Update(bool isPaused)
        {
            CheckForSnakeCollisions();
            MovePlayer();
        }

        private void CheckForSnakeCollisions()
        {
            if (fruit == null) return;
            if (_snakeBodies.First().Area.Contains(fruit.Location) || 
                fruit.Area.Contains(_snakeBodies.First().Location))
            {
                Score += fruit.Score;
                OnFruitChanged(fruit, true);
                fruit = null;
                AddBody();
                NextFruit();
            }
        }

        private void AddBody()
        {
            var snakeBody = new SnakeBody(_snakeBodies.Last().Direction, SnakeBodyType.SnakeBody);
            var x = _snakeBodies.Last().Location.X;
            var y = _snakeBodies.Last().Location.Y;

            if (_snakeBodies.Last().Direction == Direction.Right)
            {
                snakeBody.Location = new Point(x - 25, y);
            }
            else if (_snakeBodies.Last().Direction == Direction.Left)
            {
                snakeBody.Location = new Point(x + 25, y);
            }
            else if (_snakeBodies.Last().Direction == Direction.Down)
            {
                snakeBody.Location = new Point(x, y - 15);
            }
            else
            {
                snakeBody.Location = new Point(x, y + 15);
            }
            _snakeBodies.Add(snakeBody);
            OnSnakeChanged(snakeBody, false);
        }

        private void NextFruit()
        {
            int row = _random.Next(0, 10);
            int column = _random.Next(0, 15);
            Point location = new Point(column * Fruit.FruitSize.Width * 1.4, row * Fruit.FruitSize.Height * 1.4);
            switch (row)
            {
                case 0:
                    fruit = new Fruit(FruitType.Green, location, 50);
                    break;
                case 1:
                    fruit = new Fruit(FruitType.Red, location, 40);
                    break;
                case 2:
                    fruit = new Fruit(FruitType.Red, location, 30);
                    break;
                case 3:
                    fruit = new Fruit(FruitType.Green, location, 20);
                    break;
                default:
                    fruit = new Fruit(FruitType.Red, location, 10);
                    break;
            }
            OnFruitChanged(fruit, false);
        }

        public void ChangeMoveDirection(string name)
        {
            switch (name)
            {
                case "TopButton":
                    if (_snakeBodies.First().Direction == Direction.Down) return;
                    _snakeBodies.First().Direction = Direction.Up;
                    break;
                case "RightButton":
                    if (_snakeBodies.First().Direction == Direction.Left) return;
                    _snakeBodies.First().Direction = Direction.Right;
                    break;
                case "LeftButton":
                    if (_snakeBodies.First().Direction == Direction.Right) return;
                    _snakeBodies.First().Direction = Direction.Left;
                    break;
                default:
                    if (_snakeBodies.First().Direction == Direction.Up) return;
                    _snakeBodies.First().Direction = Direction.Down;
                    break;
            }
        }
    }
}

