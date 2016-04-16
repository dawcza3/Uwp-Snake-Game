using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnakeUWP.Core.Services;

namespace SnakeUWP.Core.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        #region Commands
        private INavigation navigation;

        private ICommand navigateToOptions;
        public ICommand NavigateToOptions
        {
            get
            {
                if (navigateToOptions == null)
                {
                    navigateToOptions = new RelayCommand(() =>
                    navigation.NavigateTo(typeof(OptionsViewModel)));
                }

                return navigateToOptions;
            }
        }

        private ICommand navigateToHighscore;

        public ICommand NavigateToHighscore
        {
            get
            {
                if(navigateToHighscore==null) navigateToHighscore=new RelayCommand(() =>
                {
                    navigation.NavigateTo(typeof (HighscoreViewModel));
                });
                return navigateToHighscore;
            }
        }

        private ICommand navigateToGame;

        public ICommand NavigateToGame
        {
            get
            {
                if (navigateToGame == null) navigateToGame = new RelayCommand(() =>
                {
                    navigation.NavigateTo(typeof(GameViewModel));
                });
                return navigateToGame;
            }
        }
        #endregion

        #region Constructor
        
        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }
#endregion
    }
}