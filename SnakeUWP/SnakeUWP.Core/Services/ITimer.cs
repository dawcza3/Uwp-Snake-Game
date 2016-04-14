using System;
using SnakeUWP.Core.Models;

namespace SnakeUWP.Core.Services
{
    public interface ITimer
    {
        double Interval { get; set; }
        Action OnTick { get; set; }
        void Start();
        void Stop();
        void Dispose();
    }
}