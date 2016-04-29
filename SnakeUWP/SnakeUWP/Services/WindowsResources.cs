using SnakeUWP.Core.Services;

namespace SnakeUWP.Services
{
    public class WindowsResources:IResources
    {
        public string ImageMusicOn => "ms-appx:///Assets/soundButton.png";
        public string ImageMusicOff => "ms-appx:///Assets/nosoundButton.png";
        public string ImageEasyLevel => "ms-appx:///Assets/easyLevel.png";
        public string ImageMediumLevel => "ms-appx:///Assets/mediumLevel.png";
        public string ImageHardLevel => "ms-appx:///Assets/hardLevel.png";
        public string ImagePaused => "ms-appx:///Assets/playButton.png";
        public string ImageNotPaused => "ms-appx:///Assets/stopButton.png";
    }
}