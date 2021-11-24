using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Bookish.ConsoleApp.Models;

namespace Bookish.ConsoleApp.Api
{
    public class BooksRepository
    {
        private DbHelper dbHelp = new DbHelper();
        public List<Book> GetBooks()
        {
            var sqlString = "SELECT * FROM [Books] INNER JOIN AuthorBook AB on Books.bookID = AB.bookID INNER JOIN Authors A on A.authorID = AB.authorID ORDER BY A.authorName";
            using IDbConnection db = new SqlConnection(dbHelp.GetString());
            return (List<Book>)db.Query<Book>(sqlString);
        }

        public Book GetBook(int bookId)
        {
            using IDbConnection db = new SqlConnection(dbHelp.GetString());
            var sqlString = "SELECT * FROM [Books] WHERE bookID = @BookId";
            return (Book)db.Query<Book>(sqlString,new {BookId = bookId}).Single();
        }

        public List<Author> GetAllAuthorsForBook(int bookId)
        {
            using IDbConnection db = new SqlConnection(dbHelp.GetString());
            var sqlString =
                "SELECT A.authorName , A.authorID FROM Authors A INNER JOIN AuthorBook AB on A.authorID = AB.authorID INNER JOIN Books B on AB.bookID = B.bookID WHERE B.bookID = @BookId";
            return (List<Author>)db.Query<Author>(sqlString, new {BookId = bookId});
        }
        
        
    }
}