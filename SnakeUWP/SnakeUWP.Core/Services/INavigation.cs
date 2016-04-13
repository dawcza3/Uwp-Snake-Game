using System;

namespace SnakeUWP.Core.Services
{
    public interface INavigation
    {
        void NavigateTo(Type viewModelType);
        void GoBack();
    }
}
