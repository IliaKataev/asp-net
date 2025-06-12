using BookLibrary.Pages;

namespace BookLibrary.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
    }

    public class BookService : IBookService
    {
        public IEnumerable<Book> GetBooks()
        {
            return new List<Book>
            {
                new Book {Title = "One Piece", Author = "Eichiro Oda", Genre = "Adventure saga", Publisher = "Shueisha", Year = 1997},
                new Book {Title = "Мастер и Маргарита", Author = "Булгаков МА", Genre = "Novel", Publisher = "Посев", Year = 1966},
                new Book {Title = "Monster", Author = "Naoki Urasava", Genre = "Thriller", Publisher = "Shogakukan", Year = 1994},
            };
        }
    }
}
