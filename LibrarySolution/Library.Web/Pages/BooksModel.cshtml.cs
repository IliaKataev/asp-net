using Library.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library.Data.Interfaces;

namespace Library.Web.Pages
{
    public class BooksModel : PageModel
    {
        private readonly IBookRepository _bookRepo;

        public BooksModel(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public List<Book> Books { get; private set; } = new();

        public void OnGet()
        {
            Books = _bookRepo.GetAllBooks().ToList();
        }
    }
}
