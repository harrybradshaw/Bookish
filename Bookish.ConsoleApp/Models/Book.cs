using System.Collections.Generic;
using Bookish.ConsoleApp.Models;

namespace Bookish.ConsoleApp
{
    public class Book
    {
        public int bookID;
        public string bookTitle;
        public List<Author> AuthorList;

        public void SetAuthorList(List<Author> authorList)
        {
            AuthorList = authorList;
        }
    }
}