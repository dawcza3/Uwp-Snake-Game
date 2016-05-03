using System;
using App03.Windows.Services;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SnakeUWP.Core.Services;
using SnakeUWP.Core.ViewModels;
using SnakeUWP.Services;

namespace SnakeUWP
{
    public class ViewModelLocator
    {
        public MainViewModel MainViewModel
        {
            get
            {
                // ten serwis zawsze zwraca statyczną instancję , czyli jak raz utworzymy view model,
                // to zawsze będzie ten sam view model zwracany
                //                return ServiceLocator.Current.GetInstance<MainViewModel>();

                // z keyem będzie już działać dobrze 
                // dostaniemy nowy view model zawsze
                return ServiceLocator.Current.GetInstance<MainViewModel>(GetKey());
            }
        }

        // w taki sposób jest lepiej 
        // nie zapchamy stosu ? 
        public OptionsViewModel OptionsViewModel
        {
            get
            {
                var main = ServiceLocator.Current.GetInstance<OptionsViewModel>();
                main.Cleanup();
                return main;
            }
        }

        public HighscoreViewModel HighscoreViewModel
        {
            get
            {
                var main = ServiceLocator.Current.GetInstance<HighscoreViewModel>();
                main.Cleanup();
                return main;
            }
        }
        
        public GameViewModel GameViewModel
        {
            get
            {
                var main = ServiceLocator.Current.GetInstance<GameViewModel>();
                main.Cleanup();
                return main;
            }
        }

        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<INavigation, WindowsNavigation>();
            SimpleIoc.Default.Register<ITimer,Timer>();
            SimpleIoc.Default.Register<IResources,WindowsResources>();
            SimpleIoc.Default.Register<IDatabase, WindowsDatabase>();
            SimpleIoc.Default.Register<ISettings,Settings>();


            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<OptionsViewModel>();
            SimpleIoc.Default.Register<GameViewModel>();
            SimpleIoc.Default.Register<HighscoreViewModel>();
        }

        private string GetKey()
        {
            return Guid.NewGuid().ToString();
        }

    }
}