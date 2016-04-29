
using SQLite.Net;

namespace SnakeUWP.Core.Services
{
    public interface IDatabase
    {
        SQLiteConnection Connection { get; }
    }
}
