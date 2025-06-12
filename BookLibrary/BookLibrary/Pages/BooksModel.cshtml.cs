using BookLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;

namespace BookLibrary.Pages
{
    public class BooksModel : PageModel
    {
        private readonly IBookService bookService;

        public BooksModel(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IEnumerable<Book> Books { get; set; }

        public void OnGet()
        {
            Books = bookService.GetBooks();
        }
    }
}
