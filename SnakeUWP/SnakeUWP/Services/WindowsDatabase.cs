
using System.IO;
using Windows.Storage;
using SnakeUWP.Core.Services;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;

namespace App03.Windows.Services
{
    public class WindowsDatabase : IDatabase
    {
        private SQLiteConnection connection;
        public SQLiteConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "mydatabase.sqlite");
                    connection = new SQLiteConnection(new SQLitePlatformWinRT(), path);
                }

                return connection;
            }
        }
    }
}
