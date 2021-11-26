using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Bookish.Api.Models;

namespace Bookish.Api.Api
{
    public class BooksRepository
    {
        private readonly DbHelper _dbHelp = new();
        public List<Book> GetBooks()
        {
            //var sqlString = "SELECT * FROM [Books] INNER JOIN AuthorBook AB on Books.bookID = AB.bookID INNER JOIN Authors A on A.authorID = AB.authorID ORDER BY A.authorName";
            var sqlString = "SELECT * FROM [Books]";
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            return (List<Book>)db.Query<Book>(sqlString);
        }

        public Book GetBook(int bookId)
        {
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            var sqlString = "SELECT * FROM [Books] WHERE bookID = @BookId";
            return (Book)db.Query<Book>(sqlString,new {BookId = bookId}).Single();
        }

        public List<Author> GetAllAuthorsForBook(int bookId)
        {
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            var sqlString =
                "SELECT A.authorName , A.authorID FROM Authors A INNER JOIN AuthorBook AB on A.authorID = AB.authorID INNER JOIN Books B on AB.bookID = B.bookID WHERE B.bookID = @BookId";
            return (List<Author>)db.Query<Author>(sqlString, new {BookId = bookId});
        }

        public List<Book> SearchBooks(string searchString)
        {
            var sqlString = "SELECT * FROM Books B INNER JOIN AuthorBook AB on B.bookID = AB.bookID INNER JOIN Authors A on A.authorID = AB.authorID WHERE UPPER(B.bookTitle) LIKE UPPER(@SearchString) OR UPPER(A.authorName) LIKE UPPER(@SearchString) OR UPPER(B.bookISBN) LIKE UPPER(@SearchString) ORDER BY B.bookTitle";
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            return (List<Book>)db.Query<Book>(sqlString, new {SearchString = '%'+searchString+'%'});
        }

        public List<Book> GetBooksForAuthor(string authorName, int bookId)
        {
            var sqlString = "SELECT * FROM [Books] INNER JOIN AuthorBook AB on Books.bookID = AB.bookID INNER JOIN Authors A on A.authorID = AB.authorID WHERE A.authorName = @AuthorName AND AB.bookID != @BookId ORDER BY A.authorName";
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            return (List<Book>)db.Query<Book>(sqlString, new {AuthorName = authorName, BookId = bookId});
        }

        public int AddBook(string bookTitle, string bookDesc)
        {
            using IDbConnection db = new SqlConnection(_dbHelp.GetString());
            var sqlString =
                @"INSERT INTO [Books] ([bookTitle],[bookCopies],[bookInfo]) VALUES (@BookTitle,1,@BookInfo); 
                SELECT CAST(SCOPE_IDENTITY() as int)";
            var bookId = db.QuerySingle<int>(sqlString,new { @BookTitle = bookTitle, @BookInfo = bookDesc});

            return bookId;
        }
    }
}