﻿using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SnakeUWP.Core.Services;

namespace SnakeUWP.Core.ViewModels
{
    public class HighscoreViewModel:ViewModelBase
    {
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
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}