using System;
using System.Windows.Input;
using Windows.Foundation;
using App03.Core.Repositories;
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

        public ICommand DirectionChanged
            => _directionChangedCommand ?? (_directionChangedCommand = new RelayCommand<string>((s) =>
            {
                if (!GameNotStarted)
                    _model.ChangeMoveDirection(s);
            }));

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
                              case LevelType.Easy:
                                  _timer.Interval = 86;
                                  break;
                              case LevelType.Medium:
                                  _timer.Interval = 76;
                                  break;
                              case LevelType.Hard:
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
        private UserDbRepository userDbRepository;
        private readonly INavigation _navigation;
        private IResources _resources;
        private readonly ITimer _timer;
        private readonly SnakeGameModel _model = new SnakeGameModel();
        #endregion

        #region InitializateMethods

        public void StartGame()
        {
            Paused = false;
            _model.StartGame();
            IsGamePlaying = true;
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
                    //RaisePropertyChanged("GameOver");
                    GameOver = true;
                    _timer.Stop();
                }
            }
        }

        public GameViewModel(INavigation navigation, ITimer timer, IResources resources,IDatabase database)
        {
            _navigation = navigation;
            userDbRepository = new UserDbRepository(database.Connection);
            _timer = timer;
            _resources = resources;
            _timer.OnTick = TimerTick;
            _model.SnakeChanged += _model_SnakeChanged;
            _model.FruitChanged += _model_FruitChanged;
            _pauseButtonSource = _resources.ImageNotPaused;
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

        private bool _isGamePlaying = false;

        public bool IsGamePlaying
        {
            get { return _isGamePlaying; }
            set
            {
                Set(ref _isGamePlaying, value);
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName;}
            set { Set(ref _userName, value); }
        }

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
            set
            {
                _model.GameOver = value;
                if (!GameNotStarted && _model.GameOver == true)
                {
                    IsGamePlaying = false;
                    AddUserToDataBase(UserName,_model.Score);
                    RaisePropertyChanged("GameOver");
                    RaisePropertyChanged("IsGamePlaying");
                }
            }
        }

        private void AddUserToDataBase(string userName, int score)
        {
            var user = new User()
            {
                Name = userName,
                Score=score,
                Level=Singleton.Instance.LevelType
            };
            userDbRepository.Insert(user);
        }

        private string _pauseButtonSource;
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
                    PauseButtonSource = _resources.ImagePaused;
                    if (!GameNotStarted) IsGamePlaying = false;
                    _timer.Stop();
                }
                else
                {
                    PauseButtonSource = _resources.ImageNotPaused;
                    if(!GameNotStarted) IsGamePlaying = true;
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
            Score = 0;
            GameNotStarted = true;
            GameOver = false;
            PauseButtonSource = _resources.ImageNotPaused;
            _userName = "";
            IsGamePlaying = false;
            Paused = false;
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
