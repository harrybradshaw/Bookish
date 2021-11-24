using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Bookish.ConsoleApp.Api
{
    public class BooksRepository
    {
        public List<Book> GetBooks()
        {
            var dbHelp = new DbHelper();
            var sqlString = "SELECT * FROM [Books]";
            using IDbConnection db = new SqlConnection(dbHelp.GetString());
            return (List<Book>)db.Query<Book>(sqlString);
            ;
        }

        public Book GetBook(int bookId)
        {
            var dbHelp = new DbHelper();
            using IDbConnection db = new SqlConnection(dbHelp.GetString());
            return (Book)db.Query<Book>("SELECT * FROM [Books] WHERE bookID = @BookId",new {BookId = bookId}).Single();
            ;
        }
    }
}