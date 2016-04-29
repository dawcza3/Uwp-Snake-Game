using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using App03.Core.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnakeUWP.Core.Models;
using SnakeUWP.Core.Services;

namespace SnakeUWP.Core.ViewModels
{
    public class HighscoreViewModel : ViewModelBase
    {
        private UserDbRepository userDbRepository;
        private ObservableCollection<PlayerScore> _scores;

        public ObservableCollection<PlayerScore> Scores
        {
            get
            {
                return _scores;
            }
            set { Set(ref _scores, value); }
        }

        private LevelType _currentScores = LevelType.Medium;

        public LevelType CurrentScores
        {
            get { return _currentScores; }
            set { Set(ref _currentScores, value); }
        }

        private void GetNextContent()
        {
            switch (CurrentScores)
            {
                case LevelType.Medium:
                    CurrentScores = LevelType.Hard;
                    break;
                case LevelType.Hard:
                    CurrentScores = LevelType.Easy;
                    break;
                default:
                    CurrentScores = LevelType.Medium;
                    break;
            }
            LoadScore(CurrentScores);
        }

        private async void LoadScore(LevelType scoreType)
        {
            try
            {
                List<User> _users=new List<User>();
                int place = 1;
                _users = new List<User>(
                             await userDbRepository.GetUsersWithLevel(CurrentScores));

                Scores = new ObservableCollection<PlayerScore>();
                foreach (var user in _users)
                {
                    Scores.Add(new PlayerScore(user.Name, user.Score, place));
                    place++;
                }
                while (Scores.Count < 10)
                {
                    Scores.Add(new PlayerScore("Player",0,place));
                    place++;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }

        private void GetPrevContent()
        {
            switch (CurrentScores)
            {
                case LevelType.Medium:
                    CurrentScores = LevelType.Easy;
                    break;
                case LevelType.Hard:
                    CurrentScores = LevelType.Medium;
                    break;
                default:
                    CurrentScores = LevelType.Hard;
                    break;
            }
            LoadScore(CurrentScores);
        }

        #region Commands
        private ICommand _getNextCommand;
        public ICommand GetNextCommand => _getNextCommand ?? (_getNextCommand = new RelayCommand(GetNextContent));

        private ICommand _getPrevCommand;
        public ICommand GetPrevCommand => _getPrevCommand ?? (_getPrevCommand = new RelayCommand(GetPrevContent));

        private INavigation navigation;

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
        #endregion

        public HighscoreViewModel(INavigation navigation, IDatabase database)
        {
            userDbRepository = new UserDbRepository(database.Connection);
            this.navigation = navigation;
            LoadScore(CurrentScores);
        }

        public override void Cleanup()
        {
            CurrentScores = LevelType.Medium;
            LoadScore(CurrentScores);
        }
    }
}