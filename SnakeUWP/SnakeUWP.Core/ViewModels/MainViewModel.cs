using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnakeUWP.Core.Models;
using SnakeUWP.Core.Services;

namespace SnakeUWP.Core.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        #region Commands
        private INavigation navigation;
        private ISettings settings;
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
        
        public MainViewModel(INavigation navigation,ISettings settings)
        {
            this.navigation = navigation;
            this.settings = settings;
            Singleton.Instance.LevelType=settings.LoadLevelTypeSetting(Singleton.Instance.LevelType);
            Singleton.Instance.MusicPlayed=settings.LoadMusicStatusSetting(Singleton.Instance.MusicPlayed);
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }

        #endregion
    }
}