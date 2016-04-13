
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SnakeUWP;
using SnakeUWP.Core.Services;

namespace SnakeUWP.Services
{
    public class WindowsNavigation : INavigation
    {
        public void NavigateTo(Type viewModelType)
        {
            var frame = Window.Current.Content as Frame;

            var pageTypeString = viewModelType.ToString()
                .Replace("SnakeUWP.Core.ViewModels", "SnakeUWP.Pages")
                .Replace("ViewModel", "Page");

            var pageType = Type.GetType(pageTypeString);

            frame.Navigate(pageType);
        }

        public void GoBack()
        {
            var frame = Window.Current.Content as Frame;

            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }
    }
}
