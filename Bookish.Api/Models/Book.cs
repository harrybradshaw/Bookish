using System.Collections.Generic;
using System.Linq;
using Bookish.Api.Api;

namespace Bookish.Api.Models
{
    public class Book
    {
        public int bookID;
        public string bookTitle;
        public List<Author> AuthorList = new List<Author>();
        public int BookCopies;
        public string BookCoverString;
        public string AuthorString;
        public string bookISBN;
        public int BookCopiesLeft;
        public string BookInfo;
        
        public void SetAuthorList(List<Author> authorList)
        {
            AuthorList = AuthorList.Concat(authorList).ToList();
            int i = 0;
            var tempString = "";
            foreach (var author in AuthorList)
            {
                if (i > 0)
                {
                    tempString += ", ";
                }
                tempString += author.authorName;
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