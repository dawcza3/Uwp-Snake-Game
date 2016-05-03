namespace SnakeUWP.Core.Models
{

    public sealed class Singleton
    {
        private static Singleton m_oInstance = null;

        public static Singleton Instance
        {
            get
            {
                if (m_oInstance == null)
                {
                    m_oInstance = new Singleton();
                }
                return m_oInstance;
            }
        }

        public LevelType LevelType { get; set; }

        public bool MusicPlayed { get; set; } 

    }
}