
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SnakeUWP.Core.Models;
using SnakeUWP.Core.Repositories;
using SQLite.Net;

namespace App03.Core.Repositories
{
    public class UserDbRepository : BaseDbRepository
    {
        public UserDbRepository(SQLiteConnection connection)
            : base(connection)
        {
        }

        public Task<List<User>> GetUsersWithLevel(LevelType level)
        {
            List<User> users;

            lock (databaseLock)
            {
                users = DbConnection.Table<User>().OrderByDescending(x=>x.Score).
                    Where(x=>x.Level==level).Take(10).ToList();
            }
            return Task.FromResult(users);
        }

        public void Insert(User user)
        {
            lock (databaseLock)
            {
                DbConnection.CreateTable<User>();
                DbConnection.Insert(user);
            }
        }

        public void Delete(object primaryKey)
        {
            lock (databaseLock)
            {
                DbConnection.Delete<User>(primaryKey);
            }
        }
    }
}
