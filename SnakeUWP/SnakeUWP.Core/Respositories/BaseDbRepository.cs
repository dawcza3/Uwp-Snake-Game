using SQLite.Net;

namespace SnakeUWP.Core.Repositories
{
    public class BaseDbRepository
    {
        protected static object databaseLock = new object();

        protected SQLiteConnection DbConnection { get; }

        public BaseDbRepository(SQLiteConnection connection)
        {
            DbConnection = connection;
        }
    }
}
