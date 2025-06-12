using Library.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library.Core.Models;

namespace Library.Web.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IBookRepository repo;

        public DetailsModel(IBookRepository repo)
        {
            this.repo = repo;
        }

        public Book Book { get; private set; }



        public IActionResult OnGet(int id)
        {
            Book = repo.GetBookById(id);
            if (Book == null)
                return NotFound();

            return Page();
        }
    }
}
