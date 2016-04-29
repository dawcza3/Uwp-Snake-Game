
using SQLite.Net.Attributes;

namespace SnakeUWP.Core.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Score { get; set; }

        public LevelType Level { get; set; }
    }
}
