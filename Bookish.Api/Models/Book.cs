using System.Collections.Generic;
using Bookish.Api.Api;

namespace Bookish.Api.Models
{
    public class Book
    {
        public int bookID;
        public string bookTitle;
        public List<Author> AuthorList;
        public int BookCopies;
        public string BookCoverString;
        public string AuthorString;
        public string bookISBN;
        public int BookCopiesLeft;
        
        public void SetAuthorList(List<Author> authorList)
        {
            AuthorList = authorList;
            int i = 0;
            var tempString = "";
            foreach (var author in AuthorList)
            {
                tempString += author.authorName;
                if (i > 0)
                {
                    tempString += ", ";
                }
                i++;
            }

            AuthorString = tempString;
        }

        public void SetCopiesLeft(int copies)
        {
            BookCopiesLeft = copies;
        }
        
        
    }
}