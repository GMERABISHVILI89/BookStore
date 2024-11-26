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
        private List<Book> _books;
        private List<Book> favoriteBooks = new List<Book>();
        public BookManager()
        {
            _books = new List<Book>();
        }

        public BookManager(List<Book> books)
        {
            _books = new List<Book>(books);
        }
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
            if (string.IsNullOrEmpty(searchTxt))
            {
                // Return a copy to avoid modifying original list
                return new List<Book>(books); 
            }

            searchTxt = searchTxt.Trim(); 
            int publicationYear;

            return books.Where(b =>
                b.Title.ToLower().Contains(searchTxt.ToLower()) ||
                b.Author.ToLower().Contains(searchTxt.ToLower()) ||
                (int.TryParse(searchTxt, out publicationYear) && b.PublicationYear == publicationYear)
            ).ToList();
        }

        public Book GetBookById(int id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }

        public void SaveBooksToFile(string fileName, List<Book> favoriteBooks)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            try
            {
                string json = JsonSerializer.Serialize(favoriteBooks);

                if (!File.Exists(filePath))
                {
                    // Create the file if it doesn't exist
                    using (File.Create(filePath)) { }
                }

                // Write the JSON data to the file
                File.WriteAllText(filePath, json);

                Console.WriteLine("Favorite books saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving favorite books to file: " + ex.Message);
            }
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
