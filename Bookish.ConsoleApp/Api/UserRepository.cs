using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Bookish.ConsoleApp.Models;
using Dapper;

namespace Bookish.ConsoleApp.Api
{
    public class UserRepository
    {
        private DbHelper dbHelp = new DbHelper();
        public List<User> GetAllUsers()
        {
            var sqlString = "SELECT * FROM [Users]";
            using IDbConnection db = new SqlConnection(dbHelp.GetString());
            return (List<User>)db.Query<User>(sqlString);
        }

        public User GetUserById(int id)
        {
            var sqlString = "SELECT * FROM [Users] WHERE userID = @UserId";
            using IDbConnection db = new SqlConnection(dbHelp.GetString());
            return (User)db.Query<User>(sqlString,new {UserId = id}).Single();
        }
    }
}