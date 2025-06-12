using Library.Core.Models;
using Library.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repositories
{
    public class InMemoryBookRepository : IBookRepository
    {
        private readonly List<Book> books = new()
        {
            new Book {Id = 1, Title = "One Piece", Author = "Eichiro Oda", Genre = "Adventure saga", Publisher = "Shueisha", Year = 1997},
            new Book {Id = 2, Title = "Мастер и Маргарита", Author = "Булгаков МА", Genre = "Novel", Publisher = "Посев", Year = 1966},
            new Book {Id = 3, Title = "Monster", Author = "Naoki Urasava", Genre = "Thriller", Publisher = "Shogakukan", Year = 1994},

        };
        public void AddBook(Book book) => books.Add(book);

        public IEnumerable<Book> GetAllBooks() => books;

        public Book? GetBookById(int id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }
    }
}
