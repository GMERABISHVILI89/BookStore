using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

        public void SaveBooksToFile(string filePath)
        {
            string json = JsonSerializer.Serialize(books);
            File.WriteAllText(filePath, json);
        }

        public void LoadBooksFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    books = JsonSerializer.Deserialize<List<Book>>(json);
                    Console.WriteLine("Books loaded successfully from file.");
                }
                else
                {
                    Console.WriteLine("File not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading books from file: " + ex.Message);
            }
        }

        public void DeleteBook(int id)
        {
            Book bookToDelete = books.FirstOrDefault(b => b.Id == id);
            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);
                Console.WriteLine("Book deleted successfully.");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }
    }
}
