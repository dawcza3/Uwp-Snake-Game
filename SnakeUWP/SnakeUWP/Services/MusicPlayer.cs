using Mvvm.Services.Sound;
using SnakeUWP.Core.Models;
using SnakeUWP.Core.Services;

namespace SnakeUWP.Services
{
    public class MusicPlayer:IMusicPlayer
    {
        private async void PlaySound()
        {
            await SoundPlayer.Instance.Play(true);
            SoundPlayer.Instance.IsBackgroundMuted = Singleton.Instance.MusicPlayed;
        }

        public void PlayMusic()
        {
            SoundPlayer.Instance.Initialize();
            PlaySound();
        }

        public void MuteMusic()
        {
            throw new System.NotImplementedException();
        }
    }
}