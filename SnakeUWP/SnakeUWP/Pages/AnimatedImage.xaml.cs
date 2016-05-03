using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;


// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SnakeUWP.Pages
{
    using Windows.UI.Xaml.Media.Animation;

    public sealed partial class AnimatedImage : UserControl
    {
        public AnimatedImage()
        {
            this.InitializeComponent();
        }

        public AnimatedImage(IEnumerable<string> imageNames, TimeSpan interval)
            : this()
        {
            StartAnimation(imageNames, interval);
        }

        public void StartAnimation(IEnumerable<string> imageNames, TimeSpan interval)
        {
            Storyboard storyboard = new Storyboard();
            ObjectAnimationUsingKeyFrames animation = new ObjectAnimationUsingKeyFrames();
            Storyboard.SetTarget(animation, image);
            Storyboard.SetTargetProperty(animation, "Source");

            TimeSpan currentInteval = TimeSpan.FromMilliseconds(0);
            foreach (string imageName in imageNames)
            {
                ObjectKeyFrame keyFrame = new DiscreteObjectKeyFrame();
                keyFrame.Value = CreateImageFromAssets(imageName);
                keyFrame.KeyTime = currentInteval;
                animation.KeyFrames.Add(keyFrame);
                currentInteval = currentInteval.Add(interval);
            }

            storyboard.RepeatBehavior = RepeatBehavior.Forever;
            storyboard.AutoReverse = true;
            storyboard.Children.Add(animation);
            storyboard.Begin();
        }

        public static BitmapImage CreateImageFromAssets(string imageFilename)
        {
            return new BitmapImage(new Uri("ms-appx:///Assets/" + imageFilename));
        }

    }
}
