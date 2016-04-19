using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnakeUWP.Core.Models;
using SnakeUWP.Core.Services;

namespace SnakeUWP.Core.ViewModels
{
    public class OptionsViewModel : ViewModelBase
    {
        private INavigation navigation;

        public Action<bool> OnSoundPlayChanged { get; set; }

        public OptionsViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }

        private string _musicButtonImage = "ms-appx:///Assets/soundButton.png";

        private bool _musicOption;
        public bool MusicOption
        {
            get { return _musicOption; }
            set
            {
                Set(ref _musicOption, value);
                if (value == false)
                    MusicButtonImage = "ms-appx:///Assets/soundButton.png";
                else
                    MusicButtonImage = "ms-appx:///Assets/nosoundButton.png";
            }
        }

        public string MusicButtonImage
        {
            get
            {
                return _musicButtonImage;
            }
            set { Set(ref _musicButtonImage, value); }
        }

        private string _levelButtonImage = "ms-appx:///Assets/easyLevel.png";

        public string LevelButtonImage
        {
            get { return _levelButtonImage; }
            set { Set(ref _levelButtonImage, value); }
        }

        private string _levelType="Easy Level";
        public string LevelType { get { return _levelType; } set { Set(ref _levelType, value); } }

        #region Commands

        private ICommand onBack;
        public ICommand OnBack
        {
            get
            {
                if (onBack == null)
                {
                    onBack = new RelayCommand(() => navigation.GoBack());
                }

                return onBack;
            }
        }

        private ICommand _musicCommand;

        public ICommand MusicCommand
        {
            get
            {
                if (_musicCommand == null) _musicCommand = new RelayCommand(() =>
                      {
                          MusicOption = !MusicOption;
                          OnSoundPlayChanged(MusicOption);
                      });
                return _musicCommand;
            }
        }

        private int levelCounter = 0;

        private ICommand _levelCommand;

        public ICommand LevelCommand
        {
            get
            {
                if (_levelCommand == null) _levelCommand = new RelayCommand(() =>
                {
                    if (levelCounter == 0)
                    {
                        LevelButtonImage= "ms-appx:///Assets/mediumLevel.png";
                        Singleton.Instance.LevelType = "Medium Level";
                        levelCounter++;
                    }
                    else if (levelCounter == 1)
                    {
                        LevelButtonImage = "ms-appx:///Assets/hardLevel.png";
                        Singleton.Instance.LevelType = "Hard Level";
                        levelCounter++;
                    }
                    else
                    {
                        LevelButtonImage = "ms-appx:///Assets/easyLevel.png";
                        Singleton.Instance.LevelType = "Easy Level";
                        levelCounter = 0;
                    }
                });
                return _levelCommand;
            }
        }
        #endregion

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}