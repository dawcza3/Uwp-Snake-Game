using System;
using Windows.UI.Xaml;
using SnakeUWP.Core.Services;

namespace SnakeUWP.Services
{
    public class Timer : DispatcherTimer, ITimer
    {
        #region Constructor

        public Timer()
        {
            this.Tick += OnTimerTick;
        }

        #endregion

        #region ITimer

        public new double Interval
        {
            get
            {
                return base.Interval.Milliseconds;
            }
            set
            {
                base.Interval = TimeSpan.FromMilliseconds(value);
            }
        }

        public Action OnTick
        {
            get;
            set;
        }

        public void Dispose()
        {

        }

        #endregion

        #region Methods [private]

        private void OnTimerTick(object sender, object e)
        {
            if (this.OnTick != null)
            {
                this.OnTick();
            }
        }

        #endregion
    }
}