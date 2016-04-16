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
        public ObservableCollection<PlayerScore> MediumScores { get; private set; }

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
            }
            else if (CurrentScores == "High Level")
            {
                CurrentScores = "Easy Level";
            }
            else
            {
                CurrentScores = "Medium Level";
            }
        }
        private void GetPrevContent()
        {
            if (CurrentScores == "Medium Level")
            {
                CurrentScores = "Easy Level";
            }
            else if (CurrentScores == "High Level")
            {
                CurrentScores = "Medium Level";
            }
            else
            {
                CurrentScores = "High Level";
            }
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
            MediumScores = new ObservableCollection<PlayerScore>
            {
                new PlayerScore("Dawid", 3100,1),
                new PlayerScore("Kasia", 2200,2),
                new PlayerScore("Marek", 1050,3),
                new PlayerScore("Asia", 500,4),
                new PlayerScore("Mirek", 60,5),
                new PlayerScore("Konrad", 2500,6),
                new PlayerScore("Dariusz", 2200,7),
                new PlayerScore("Ola", 4100,8),
                new PlayerScore("Tomek", 5000,9),
                new PlayerScore("Adam", 10,10)  
            };
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}