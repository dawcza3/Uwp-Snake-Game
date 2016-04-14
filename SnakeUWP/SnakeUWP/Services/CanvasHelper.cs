using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using SnakeUWP.Core.Models;
using SnakeUWP.Pages;

namespace SnakeUWP.Services
{
    public class CanvasHelper
    {
        internal static FrameworkElement FruitControlFactory(Fruit fruit, double scale)
        {
            IEnumerable<string> imageNames = CreateImageList(fruit.MyFruitType);
            AnimatedImage fruitControl = new AnimatedImage(imageNames, TimeSpan.FromSeconds(.75));
            fruitControl.Width = fruit.Size.Width * scale;
            fruitControl.Height = fruit.Size.Height * scale;
            SetCanvasLocation(fruitControl, fruit.Location.X * scale, fruit.Location.Y * scale);
            return fruitControl;
        }

        internal static FrameworkElement SnakeControlFactory(SnakeBody snakeHead, double scale)
        {
            AnimatedImage snakeControl = new AnimatedImage(new List<string>() { "player.png" }, TimeSpan.FromSeconds(1));
            snakeControl.Width = snakeHead.Size.Width * scale;
            snakeControl.Height = snakeHead.Size.Height * scale;
            SetCanvasLocation(snakeControl, snakeHead.Location.X * scale, snakeHead.Location.Y * scale);
            return snakeControl;
        }

        public static void SetCanvasLocation(FrameworkElement control, double x, double y)
        {
            Canvas.SetLeft(control, x);
            Canvas.SetTop(control, y);
        }

        public static void MoveElementOnCanvas(FrameworkElement FrameworkElement, double toX, double toY)
        {
            double fromX = Canvas.GetLeft(FrameworkElement);
            double fromY = Canvas.GetTop(FrameworkElement);

            Storyboard storyboard = new Storyboard();
            DoubleAnimation animationX = CreateDoubleAnimation(FrameworkElement,
                                                fromX, toX, "(Canvas.Left)");
            DoubleAnimation animationY = CreateDoubleAnimation(FrameworkElement,
                                                fromY, toY, "(Canvas.Top)");
            storyboard.Children.Add(animationX);
            storyboard.Children.Add(animationY);
            storyboard.Begin();
        }

        public static DoubleAnimation CreateDoubleAnimation(FrameworkElement frameworkElement,
                                     double from, double to, string propertyToAnimate)
        {
            return CreateDoubleAnimation(frameworkElement, from, to, propertyToAnimate, TimeSpan.FromMilliseconds(25));
        }

        public static DoubleAnimation CreateDoubleAnimation(FrameworkElement frameworkElement,
                                             double from, double to, string propertyToAnimate, TimeSpan timeSpan)
        {
            DoubleAnimation animation = new DoubleAnimation();
            Storyboard.SetTarget(animation, frameworkElement);
            Storyboard.SetTargetProperty(animation, propertyToAnimate);
            animation.From = from;
            animation.To = to;
            animation.Duration = timeSpan;
            return animation;
        }

        public static void ResizeElement(FrameworkElement control, double width, double height)
        {
            if (control.Width != width)
                control.Width = width;
            if (control.Height != height)
                control.Height = height;
        }

        private static IEnumerable<string> CreateImageList(FruitType shipType)
        {
            string filename;
            switch (shipType)
            {
                case FruitType.Red:
                    filename = "red";
                    break;
                case FruitType.Green:
                    filename = "green";
                    break;
                default:
                    filename = "red";
                    break;
            }
            List<string> imageList = new List<string>();
            /*for (int i = 1; i <= 4; i++)
                imageList.Add(filename + i + ".png");*/
            imageList.Add(filename + ".png");
            return imageList;
        }
    }
}