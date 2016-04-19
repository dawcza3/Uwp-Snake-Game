using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnakeUWP.Core.Models;
using SnakeUWP.Core.Services;

namespace SnakeUWP.Core.ViewModels
{
    public class HighscoreViewModel : ViewModelBase
    {
        private ObservableCollection<PlayerScore> _scores;

        public ObservableCollection<PlayerScore> Scores
        {
            get
            {
                return _scores;
            } 
            set { Set(ref _scores , value); }
        }

        private string _currentScores = "Medium Level";

        public string CurrentScores
        {
            get { return _currentScores; }
            set { Set(ref _currentScores, value); }
        }

        private void GetNextContent()
        {
            if (CurrentScores == "Medium Level")
            {
                CurrentScores = "High Level";
                LoadScore("High");
            }
            else if (CurrentScores == "High Level")
            {
                CurrentScores = "Easy Level";
                LoadScore("Easy");
            }
            else
            {
                CurrentScores = "Medium Level";
                LoadScore("Medium");
            }
        }

        private void LoadScore(string scoreType)
        {
            switch (scoreType)
            {
                case "Easy":
                    Scores = new ObservableCollection<PlayerScore>
                    {
                        new PlayerScore("Józef", 5100, 1),
                        new PlayerScore("Zenon", 3500, 2),
                        new PlayerScore("Kasia", 2200, 3),
                        new PlayerScore("Asia", 2200, 4),
                        new PlayerScore("Dariusz", 1050, 5),
                        new PlayerScore("Zenon", 1000, 8),
                        new PlayerScore("Asia", 500, 6),
                        new PlayerScore("Mirek", 60, 7),
                        new PlayerScore("Tomek", 30, 9),
                        new PlayerScore("Adam", 10, 10)
                    };
                    break;
                case "Medium":
                    Scores = new ObservableCollection<PlayerScore>
                    {
                        new PlayerScore("Dawid", 3100, 1),
                        new PlayerScore("Konrad", 2500, 2),
                        new PlayerScore("Kasia", 2200, 3),
                        new PlayerScore("Dariusz", 2200, 4),
                        new PlayerScore("Marek", 1050, 5),
                        new PlayerScore("Ola", 1000, 8),
                        new PlayerScore("Asia", 500, 6),
                        new PlayerScore("Mirek", 60, 7),
                        new PlayerScore("Tomek", 30, 9),
                        new PlayerScore("Adam", 10, 10)
                    };
                    break;
                case "High":
                    Scores = new ObservableCollection<PlayerScore>
                    {
                        new PlayerScore("Józef", 2100, 1),
                        new PlayerScore("Zenon", 1500, 2),
                        new PlayerScore("Kasia", 1200, 3),
                        new PlayerScore("Asia", 1200, 4),
                        new PlayerScore("Dariusz", 550, 5),
                        new PlayerScore("Zenon", 400, 8),
                        new PlayerScore("Asia", 300, 6),
                        new PlayerScore("Mirek", 260, 7),
                        new PlayerScore("Tomek", 230, 9),
                        new PlayerScore("Adam", 100, 10)
                    };
                    break;
            }
        }

        private void GetPrevContent()
        {
            if (CurrentScores == "Medium Level")
            {
                CurrentScores = "Easy Level";
                LoadScore("Easy");
            }
            else if (CurrentScores == "High Level")
            {
                CurrentScores = "Medium Level";
                LoadScore("Medium");
            }
            else
            {
                CurrentScores = "High Level";
                LoadScore("High");
            }
            RaisePropertyChanged("Scores");
        }

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

        public HighscoreViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            Scores = new ObservableCollection<PlayerScore>
            {
                new PlayerScore("Dawid", 3100, 1),
                new PlayerScore("Konrad", 2500, 2),
                new PlayerScore("Kasia", 2200, 3),
                new PlayerScore("Dariusz", 2200, 4),
                new PlayerScore("Marek", 1050, 5),
                new PlayerScore("Ola", 1000, 8),
                new PlayerScore("Asia", 500, 6),
                new PlayerScore("Mirek", 60, 7),
                new PlayerScore("Tomek", 30, 9),
                new PlayerScore("Adam", 10, 10)
            };
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}