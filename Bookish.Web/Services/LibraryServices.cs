using System.Collections.Generic;
using Bookish.Api.Models;
using Bookish.Api.Api;

namespace Bookish.Web.Services
{
    public class LibraryServices
    {
        private readonly BooksRepository _booksRepository = new BooksRepository();
        private readonly LoansRepository _loansRepository = new();
        private readonly UserRepository _userRepository = new();
        private readonly AuthorRepository _authorRepository = new();
        public Books GetBooks()
        {
            var books = new Books();
            foreach (var book in books.BookList)
            {
                int copies = _loansRepository.OnLoanByBookId(book.bookID);
                book.SetCopiesLeft(copies);
            }
            return books;
        }

        public Book GetBook(int bookId)
        {
            var book = _booksRepository.GetBook(bookId);
            book.SetAuthorList(_booksRepository.GetAllAuthorsForBook(bookId));
            return book;
        }

        public Loans GetLoansByBook(Book book)
        {
            Loans loans = new Loans(book);
            return loans;
        }

        public Loans GetLoansByUserId(int userId)
        {
            User user = _userRepository.GetUserById(userId);
            Loans loans = new Loans(user);
            return loans;
        }
        
        public bool Checkout(int userId, int bookId)
        {
            var book = _booksRepository.GetBook(bookId);
            if (book.BookCopies - _loansRepository.OnLoanByBookId(bookId) >= 1)
            {
                _loansRepository.CheckoutBook(userId,bookId);
                return true;
            }

            return false;
        }
        
        public bool Checkin(int loanId)
        {
            return _loansRepository.CheckinBook(loanId);
        }

        public Books SearchLibrary(string searchString)
        {
            Books books = new Books();
            books.BookList = _booksRepository.SearchBooks(searchString);
            foreach (var book in books.BookList)
            {
                book.SetAuthorList(_booksRepository.GetAllAuthorsForBook(book.bookID));
            }
            return books;
        }

        public Books GetBooksByAuthorNotInc(int bookId, List<Author> authorList)
        {
            Books booksByAuthor = new Books();
            foreach (var author in authorList)
            {
                booksByAuthor.BookList = _booksRepository.GetBooksForAuthor(author.authorName, bookId);
            }
            foreach (var book in booksByAuthor.BookList)
            {
                book.SetAuthorList(_booksRepository.GetAllAuthorsForBook(book.bookID));
            }
            return booksByAuthor;
        }

        public bool AddBookFromRequest(string bookTitle, string bookAuthors, string bookDesc)
        {
            var authorList = bookAuthors.Split(",");
            var authorIdList = new List<int>();
            foreach (var author in authorList)
            {
                var tempString = author.Trim();
                var tempId = _authorRepository.AuthorExists(author);
                if (tempId == -1)
                {
                    authorIdList.Add(_authorRepository.AddAuthor(author));
                }
                else
                {
                    authorIdList.Add(tempId);
                }
            }
            var bookId = _booksRepository.AddBook(bookTitle, bookDesc);

            foreach (var authorId in authorIdList)
            {
                _authorRepository.AddAuthorBook(authorId,bookId);
            }
            
            return true;
        }
        
    }
}