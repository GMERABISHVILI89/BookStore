using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public class BookManager
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            books.Add(book);

        }

        public
        List<Book> GetAllBooks()
        {
            return books;
        }

        public List<Book> SearchBooks(string searchTxt)
        {
            return books.Where(b => b.Title.Contains(searchTxt) || b.Author.Contains(searchTxt) || b.PublicationYear.ToString().Contains(searchTxt)).ToList();
        }

        public Book GetBookById(int id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }
    }
}
