using System;
using System.Windows.Input;
using Windows.Foundation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnakeUWP.Core.Models;
using SnakeUWP.Core.Services;

namespace SnakeUWP.Core.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        #region Commands
        private ICommand _onBack;
        public ICommand OnBack
        {
            get
            {
                if (_onBack == null)
                {
                    Paused = false;
                    _onBack = new RelayCommand(() => _navigation.GoBack());
                }

                return _onBack;
            }
        }

        private ICommand _directionChangedCommand;
        public ICommand DirectionChanged => _directionChangedCommand ?? (_directionChangedCommand = new RelayCommand<string>
            (_model.ChangeMoveDirection));

        private ICommand _pauseCommand;

        public ICommand PauseCommand
        {
            get
            {
                if (_pauseCommand == null)
                {
                    _pauseCommand = new RelayCommand(() =>
                    {
                        if (!GameOver)
                            Paused = !Paused;

                    });
                }
                return _pauseCommand;
            }
        }

        private ICommand _startGameCommand;

        private string _name;
        public ICommand StartGameCommand
        {
            get
            {
                if (_startGameCommand == null) _startGameCommand = new RelayCommand<string>((name) =>
                      {
                          _name = name;
                          switch (Singleton.Instance.LevelType)
                          {
                              case "Easy Level":
                                  _timer.Interval = 86;
                                  break;
                              case "Medium Level":
                                  _timer.Interval = 76;
                                  break;
                              case "Hard Level":
                                  _timer.Interval = 66;
                                  break;
                          }
                          _timer.Start();
                          _model.EndGame();
                          StartGame();
                          GameNotStarted = false;
                      }, (name) =>
                      {
                          if (name == null) return false;
                          if (name.Length != 0)
                              return true;
                          else
                              return false;
                      });
                return _startGameCommand;
            }
        }

        #endregion

        #region Fields
        private readonly INavigation _navigation;
        private readonly ITimer _timer;
        private readonly SnakeGameModel _model = new SnakeGameModel();
        #endregion

        #region InitializateMethods

        public void StartGame()
        {
            Paused = false;
            _model.StartGame();
            RaisePropertyChanged("GameOver");

        }

        private void TimerTick()
        {
            if (!GameNotStarted)
            {
                _model.Update(false);

                if (Score != _model.Score)
                {
                    Score = _model.Score;
                    RaisePropertyChanged("Score");
                }

                if (_model.GameOver)
                {
                    RaisePropertyChanged("GameOver");
                    _timer.Stop();
                }
            }
        }

        public GameViewModel(INavigation navigation, ITimer timer)
        {
            _navigation = navigation;
            _timer = timer;
            _timer.OnTick = TimerTick;
            _model.SnakeChanged += _model_SnakeChanged;
            _model.FruitChanged += _model_FruitChanged;
        }
        #endregion

        #region EventHandlers
        public Action<SnakeChangedEventArgs> UpdateSnakeAction { get; set; }

        public Action<FruitChangedEventArgs> UpdateFruitAction { get; set; }

        private void _model_FruitChanged(object sender, FruitChangedEventArgs e)
        {
            UpdateFruitAction?.Invoke(e);
        }

        private void _model_SnakeChanged(object sender, SnakeChangedEventArgs e)
        {
            UpdateSnakeAction?.Invoke(e);
        }
        #endregion

        #region Properties

        private bool _gameNotStarted = true;

        public bool GameNotStarted
        {
            get
            {
                return _gameNotStarted;
            }
            set
            {
                Set(ref _gameNotStarted, value);
            }
        }

        public bool GameOver
        {
            get { return _model.GameOver; }
            set { _model.GameOver = value; }
        }

        private string _pauseButtonSource = "ms-appx:///Assets/stopButton.png";
        public string PauseButtonSource
        {
            get { return _pauseButtonSource; }
            set { Set(ref _pauseButtonSource, value); }
        }

        private bool _paused;

        public bool Paused
        {
            get
            {
                return _paused;
            }
            set
            {
                Set(ref _paused, value);
                if (value)
                {
                    PauseButtonSource = "ms-appx:///Assets/playButton.png";
                    _timer.Stop();
                }
                else
                {
                    PauseButtonSource = "ms-appx:///Assets/stopButton.png";
                    _timer.Start();
                }
            }
        }

        public static double Scale { get; set; } = 1;

        public int Score { get; private set; }

        public Size PlayAreaSize
        {
            set
            {
                Scale = (value.Height / 300 + value.Width / 300) / 2;
                //SnakeBody.PlayAreaSize.Width = value.Width;
                //SnakeBody.PlayAreaSize.Height = value.Height;
            }
        }

        public override void Cleanup()
        {
            base.Cleanup();
            //_timer.Start();
            //_model.EndGame();
            Score = 0;
            //StartGame();
            GameNotStarted = true;
            GameOver = false;
            PauseButtonSource = "ms-appx:///Assets/stopButton.png";
        }

        #endregion

        #region Methods

        public void ChangedDirection(string direction)
        {
            _model.ChangeMoveDirection(direction);
        }
        #endregion

    }
}
