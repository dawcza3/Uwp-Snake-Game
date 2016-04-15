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
                new PlayerScore("Dawid", 1500),
                new PlayerScore("Kasia", 2200),
                new PlayerScore("Marek", 3100),
                new PlayerScore("Tomek", 1000),
                new PlayerScore("Mirek", 1200),

/*                new PlayerScore("Dawid", 1500),
                new PlayerScore("Dawid", 1500),
                new PlayerScore("Dawid", 1500),
                new PlayerScore("Dawid", 1500),
                new PlayerScore("Dawid", 1500),*/
            };
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}