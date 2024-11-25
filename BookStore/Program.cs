
using System.Net.Http.Headers;
using System.Text.Json;

namespace BookStore
{

    class Program
    {
      
        static async Task Main(string[] args)
        {
            BookManager bookManager = new BookManager();

            using (HttpClient client = new HttpClient())
            {
               // string apiUrl = "https://jsonplaceholder.typicode.com/posts";
               // string responseBody = await client.GetStringAsync(apiUrl);

                //List<Book> books = JsonSerializer.Deserialize<List<Book>>(responseBody);

                // Take the first 15 books
                // books = books.Take(15).ToList();

                // data of popular authors to their books and publication years
                List<Book> popularAuthors = new List<Book>()
                {
                new Book {Id = 1, Author = "J.R.R. Tolkien", Title = "The Lord of the Rings", PublicationYear = 1954 },
                new Book {Id = 2, Author = "J.R.R. Tolkien", Title = "The Hobbit", PublicationYear = 1937 },
                new Book {Id = 3, Author = "Stephen King", Title = "The Shining", PublicationYear = 1977 },
                new Book {Id = 4, Author = "Stephen King", Title = "It", PublicationYear = 1986 },
                new Book {Id = 5, Author = "J.K. Rowling", Title = "Harry Potter and the Sorcerer's Stone", PublicationYear = 1997 },
                new Book {Id = 6, Author = "J.K. Rowling", Title = "Harry Potter and the Chamber of Secrets", PublicationYear = 1998 },
                new Book {Id = 7, Author = "Agatha Christie", Title = "Murder on the Orient Express", PublicationYear = 1934 },
                new Book {Id = 8, Author = "Agatha Christie", Title = "And Then There Were None", PublicationYear = 1939 },
                new Book {Id = 9, Author = "Dan Brown", Title = "The Da Vinci Code", PublicationYear = 2003 },
                new Book {Id = 10, Author = "Dan Brown", Title = "Angels & Demons", PublicationYear = 2000 },
                new Book {Id = 11, Author = "Jane Austen", Title = "Pride and Prejudice", PublicationYear = 1813 },
                new Book {Id = 12, Author = "Jane Austen", Title = "Sense and Sensibility", PublicationYear = 1811 },
                new Book {Id = 13, Author = "Fyodor Dostoevsky", Title = "Crime and Punishment", PublicationYear = 1866 },
                new Book {Id = 14, Author = "Fyodor Dostoevsky", Title = "The Brothers Karamazov", PublicationYear = 1880 },
                new Book {Id = 15, Author = "Leo Tolstoy", Title = "War and Peace", PublicationYear = 1869 }
                };
          
                for (int i = 0; i < 15; i++)
                {
                  //  books[i] = popularAuthors[i];

                    // წიგნების ლისტში დამატება
                    bookManager.AddBook(popularAuthors[i]);
                }

            }

         while (true)
               {
                   Console.WriteLine("1. Add Book");
                   Console.WriteLine("2. Search Book");
                   Console.WriteLine("3. List All Books");
                   Console.WriteLine("4. Exit");
                    
                    
                   Console.Write("Enter your choice: ");
                   int.TryParse(Console.ReadLine(), out int choice);

                   switch (choice)
                   {
                       case 1:
                           Console.Write("Enter book title: ");
                           var title = Console.ReadLine();
                           Console.Write("Enter author name: ");
                           var author = Console.ReadLine();
                           Console.Write("Enter publication year: ");
                           int.TryParse(Console.ReadLine(), out int year);

                           Book newBook = new Book
                            {
                               Id = bookManager.GetAllBooks().Count + 1,
                               Title = title,
                               Author = author,
                               PublicationYear = year
                           };

                           bookManager.AddBook(newBook);
                           break;
                       case 2:
                           Console.Write("Enter book Name or anything to search: ");
                            var search = Console.ReadLine();
                           var foundBook = bookManager.SearchBooks(search);
                            if (foundBook.Any())
                            {
                            foreach (var book in foundBook)
                            {
                                Console.WriteLine($"Book found: {book.Title} by {book.Author}");
                            }
                             }
                        else
                        {
                            Console.WriteLine("No books found matching your search criteria.");
                        }
                        break;
                       case 3:
                           List<Book> allBooks = bookManager.GetAllBooks();
                           foreach (Book book in allBooks)
                           {
                               Console.WriteLine($"  Title: {book.Title}, Author: {book.Author}, Year: {book.PublicationYear}");
                           }
                           break;
                    case 4:
                        // Edit Book
                        Console.Write("Enter the ID of the book to edit: ");
                        int bookId = int.Parse(Console.ReadLine());

                        Book bookToEdit = bookManager.GetBookById(bookId);
                        if (bookToEdit != null)
                        {
                            Console.Write("Enter new title (or press Enter to keep the same): ");
                            string newTitle = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newTitle))
                            {
                                bookToEdit.Title = newTitle;
                            }


                            Console.WriteLine("Book updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Book not found.");
                        }
                        break;

                       case 5:
                           Environment.Exit(0);
                           break;
                       default:
                           Console.WriteLine("Invalid choice. Please try again.");
                           break;
                   }
               }
        }

    }
 }