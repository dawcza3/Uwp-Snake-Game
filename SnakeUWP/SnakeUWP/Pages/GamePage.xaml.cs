using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using SnakeUWP.Core.Models;
using SnakeUWP.Core.Services;
using SnakeUWP.Core.ViewModels;
using SnakeUWP.Services;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SnakeUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private double _scale = 1;
        private readonly ObservableCollection<FrameworkElement>
    _sprites = new ObservableCollection<FrameworkElement>();

        public INotifyCollectionChanged Sprites => _sprites;

        private readonly Dictionary<SnakeBody, FrameworkElement> _snakeBodies = new Dictionary<SnakeBody, FrameworkElement>();

        private FrameworkElement _fruitFrameworkElement = null;
        private Fruit _fruit = null;

        public GamePage()
        {
            this.InitializeComponent();
            PlayControl.ItemsSource = Sprites;
            var gameViewModel = DataContext as GameViewModel;
            if (gameViewModel != null)
            {
                gameViewModel.UpdateSnakeAction += UpdateSnake;
                gameViewModel.UpdateFruitAction += UpdateFruit;
                gameViewModel.StartGame();
            }
        }

        private void UpdateFruit(FruitChangedEventArgs e)
        {
            if (!e.IsEaten)
            {
                _fruit = e.FruitUpdated;

                _fruitFrameworkElement = CanvasHelper.FruitControlFactory(_fruit, _scale);
                _sprites.Add(_fruitFrameworkElement);
                CanvasHelper.ResizeElement(_fruitFrameworkElement, _fruit.Size.Width * _scale, _fruit.Size.Height * _scale);
            }
            else
            {
                this._fruit = null;
                _sprites.Remove(_fruitFrameworkElement);
                _fruitFrameworkElement = null;
            }
        }

        private void UpdateSnake(SnakeChangedEventArgs e)
        {
            if (!e.Killed)
            {
                var snakePart = e.SnakeUpdated;
                if (!_snakeBodies.ContainsKey(snakePart))
                {
                    FrameworkElement invaderControl = CanvasHelper.SnakeControlFactory(snakePart,
                        _scale);
                    _snakeBodies[snakePart] = invaderControl;
                    _sprites.Add(invaderControl);
                }
                else
                {
                    FrameworkElement invaderControl = _snakeBodies[snakePart];
                    CanvasHelper.MoveElementOnCanvas(invaderControl, snakePart.Location.X * _scale, snakePart.Location.Y * _scale);
                    CanvasHelper.ResizeElement(invaderControl, snakePart.Size.Width * _scale, snakePart.Size.Height * _scale);
                }
            }
        }

        private void pageRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var gameViewModel = DataContext as GameViewModel;
            var newPlayAreaSize = e.NewSize;
            newPlayAreaSize.Width -= 160;
            newPlayAreaSize.Height -= 104;
            if (gameViewModel != null)
            {
                _scale = (newPlayAreaSize.Height / 300 + newPlayAreaSize.Width / 300) / 2;
                GameViewModel.Scale = _scale;
                if (this._fruit != null && this._fruitFrameworkElement != null)
                {
                    CanvasHelper.MoveElementOnCanvas(_fruitFrameworkElement, _fruit.Location.X * _scale, _fruit.Location.Y * _scale);
                    CanvasHelper.ResizeElement(_fruitFrameworkElement, _fruit.Size.Width * _scale, _fruit.Size.Height * _scale);
                }
            }
        }

    }
}

