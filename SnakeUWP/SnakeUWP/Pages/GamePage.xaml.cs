using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
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
        private Size newPlayAreaSize;
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
                //gameViewModel.StartGame();
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
                    FrameworkElement snakeControl = CanvasHelper.SnakeControlFactory(snakePart,
                        _scale);
                    _snakeBodies[snakePart] = snakeControl;
                    _sprites.Add(snakeControl);
                }
                else
                {
                    FrameworkElement snakeControl = _snakeBodies[snakePart];
                    CanvasHelper.ResizeElement(snakeControl, snakePart.Size.Width * _scale, snakePart.Size.Height * _scale);
                    CanvasHelper.MoveElementOnCanvas(snakeControl, snakePart.Location.X * _scale, snakePart.Location.Y * _scale);
                }
            }
        }

        private void pageRoot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var gameViewModel = DataContext as GameViewModel;
            newPlayAreaSize = e.NewSize;
            newPlayAreaSize.Width -= 208; // odejmuje wielkosci kolumn + grubosc ramki 
            newPlayAreaSize.Height -= 152+8; // odjemuje wielkosci wierszy + grubosc ramki

            if (gameViewModel != null)
            {
                _scale = (newPlayAreaSize.Height / 300 + newPlayAreaSize.Width / 300) / 2;
                SnakeBody.PlayAreaSize = newPlayAreaSize;
                SnakeBody.Scale = _scale;
                GameViewModel.Scale = _scale;
                if (this._fruit != null && this._fruitFrameworkElement != null)
                {
                    CanvasHelper.MoveElementOnCanvas(_fruitFrameworkElement, _fruit.Location.X * _scale, _fruit.Location.Y * _scale);
                    CanvasHelper.ResizeElement(_fruitFrameworkElement, _fruit.Size.Width * _scale, _fruit.Size.Height * _scale);
                }
            }
        }

        private void UIElement_OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            var viewmodel = (DataContext as GameViewModel);
            if (viewmodel == null) return;
            switch (e.Key)
            {
                case VirtualKey.Left:
                    viewmodel.ChangedDirection("LeftButton");
                    break;
                case VirtualKey.Right:
                    viewmodel.ChangedDirection("RightButton");
                    break;
                case VirtualKey.Down:
                    viewmodel.ChangedDirection("BottomButton");
                    break;
                case VirtualKey.Up:
                    viewmodel.ChangedDirection("TopButton");
                    break;
                default:
                    return;
            }
        }
    }
}

