using Kino_Pesochnisa_16._06._25.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Kino_Pesochnisa_16._06._25.Pages
{
    public class FilmDetailsModel : PageModel
    {
        private readonly string path;

        public FilmDetailsModel(IWebHostEnvironment env)
        {
            path = Path.Combine(env.ContentRootPath, "AppData", "films.json");
        }

        public IWatch Film {  get; set; }


        public IActionResult OnGet(int id)
        {
            var json = System.IO.File.ReadAllText(path);
            var films = JsonSerializer.Deserialize<List<IWatch>>(json) ?? new();

            Film = films.FirstOrDefault(f => f.Id == id);

            if (Film == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            var json = System.IO.File.ReadAllText(path);
            var films = JsonSerializer.Deserialize<List<IWatch>>(json) ?? new();

            var filmToDelete = films.FirstOrDefault(f => f.Id == id);

            if(filmToDelete == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(filmToDelete.ImagePath))
            {
                var fullImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filmToDelete.ImagePath);
                if (System.IO.File.Exists(fullImagePath))
                {
                    System.IO.File.Delete(fullImagePath);
                }

            }

            films.Remove(filmToDelete);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All)
            };

            var updatedJson = JsonSerializer.Serialize(films, options);
            System.IO.File.WriteAllText(path, updatedJson);

            return RedirectToPage("/Film");
        }
    }

}
