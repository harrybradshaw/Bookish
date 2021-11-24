using System.Collections.Generic;

namespace Bookish.ConsoleApp.Models
{
    public class Book
    {
        public int bookID;
        public string bookTitle;
        public List<Author> AuthorList;
        public int BookCopies;
        

        public void SetAuthorList(List<Author> authorList)
        {
            AuthorList = authorList;
        }
    }
}