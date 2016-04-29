using System;
using System.Windows.Input;
using App03.Core.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnakeUWP.Core.Models;
using SnakeUWP.Core.Services;

namespace SnakeUWP.Core.ViewModels
{
    public class OptionsViewModel : ViewModelBase
    {
        private void LoadSettings()
        {
            var levelType = Singleton.Instance.LevelType;
            switch (levelType)
            {
                case LevelType.Easy:
                    LevelButtonImage = resources.ImageEasyLevel;
                    break;
                case LevelType.Medium:
                    LevelButtonImage = resources.ImageMediumLevel;
                    break;
                case LevelType.Hard:
                    LevelButtonImage = resources.ImageHardLevel;
                    break;
            }
            MusicOption = Singleton.Instance.MusicPlayed;
        }

        private void SaveSettings()
        {
            settings.SaveSettings(Singleton.Instance.LevelType, Singleton.Instance.MusicPlayed);
        }

        private INavigation navigation;
        private IResources resources;
        private ISettings settings;
        public Action<bool> OnSoundPlayChanged { get; set; }

        public OptionsViewModel(INavigation navigation, IResources resources, ISettings settings)
        {
            this.navigation = navigation;
            this.resources = resources;
            this.settings = settings;
            _musicButtonImage = resources.ImageMusicOn;
            _levelButtonImage = resources.ImageEasyLevel;
            LoadSettings();
        }

        #region Properties
        private string _musicButtonImage;

        private bool _musicOption;
        public bool MusicOption
        {
            get { return _musicOption; }
            set
            {
                Set(ref _musicOption, value);
                if (value == false) 
                    MusicButtonImage = resources.ImageMusicOn;
                else
                    MusicButtonImage = resources.ImageMusicOff;
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

        private string _levelButtonImage;

        public string LevelButtonImage
        {
            get { return _levelButtonImage; }
            set { Set(ref _levelButtonImage, value); }
        }

        private LevelType _levelType = Models.LevelType.Easy;
        public LevelType LevelType { get { return _levelType; } set { Set(ref _levelType, value); } }

        #endregion

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
                          Singleton.Instance.MusicPlayed = MusicOption;
                          OnSoundPlayChanged(MusicOption);
                          SaveSettings();
                      });
                return _musicCommand;
            }
        }

        private ICommand _levelCommand;

        public ICommand LevelCommand
        {
            get
            {
                if (_levelCommand == null) _levelCommand = new RelayCommand(() =>
                {
                    var status = Singleton.Instance.LevelType;
                    switch (status)
                    {
                        case LevelType.Easy:
                            LevelButtonImage = resources.ImageMediumLevel;
                            Singleton.Instance.LevelType = Models.LevelType.Medium;
                            break;
                        case LevelType.Medium:
                            LevelButtonImage = resources.ImageHardLevel;
                            Singleton.Instance.LevelType = Models.LevelType.Hard;
                            break;
                        case LevelType.Hard:
                            LevelButtonImage = resources.ImageEasyLevel;
                            Singleton.Instance.LevelType = Models.LevelType.Easy;
                            break;
                    }
                    SaveSettings();
                });
                return _levelCommand;
            }
        }
        #endregion

        public override void Cleanup()
        {
            base.Cleanup();
            LoadSettings();
        }
    }
}