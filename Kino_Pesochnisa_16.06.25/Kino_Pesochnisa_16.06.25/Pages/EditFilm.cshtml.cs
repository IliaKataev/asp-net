using Kino_Pesochnisa_16._06._25.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Kino_Pesochnisa_16._06._25.Pages
{
    public class EditFilmModel : PageModel
    {
        private readonly string path;
        private readonly string imagePath;

        public EditFilmModel(IWebHostEnvironment webHostEnvironment)
        {
            path = Path.Combine(webHostEnvironment.ContentRootPath, "AppData", "films.json");
            imagePath = Path.Combine(webHostEnvironment.WebRootPath, "images");
            
        }

        [BindProperty]
        public IWatch Film { get; set; } = new();

        [BindProperty]
        public IFormFile? UploadedImage { get; set; }

        [BindProperty]
        public string SeanceString { get; set; } = "";

        public IActionResult OnGet(int id)
        {
            var films = ReadFromFile();
            var film = films.FirstOrDefault(f => f.Id == id);

            if(film == null)
            {
                return NotFound();
            }

            Film = film;
            SeanceString = string.Join(", ", film.Seances);

            return Page();
        }

        public IActionResult OnPost()
        {
            var films = ReadFromFile();
            var index = films.FindIndex(f => f.Id == Film.Id);
            
            if(index == -1)
            {
                return NotFound();
            }
            Film.Seances = SeanceString.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();

            if (UploadedImage != null)
            {
                var filename = $"{Guid.NewGuid()}{Path.GetExtension(UploadedImage.FileName)}";
                var filePath = Path.Combine(imagePath, filename);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    UploadedImage.CopyTo(stream);
                }

                Film.ImagePath = $"/images/{filename}";
            }
            else
            {
                Film.ImagePath = films[index].ImagePath;
            }

            films[index] = Film;
            SaveToFile(films);

            return RedirectToPage("/FilmDetails", new {id = Film.Id});
        }

        private List<IWatch> ReadFromFile()
        {
            var json = System.IO.File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<IWatch>>(json) ?? new();
        }

        private void SaveToFile(List<IWatch> films)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            var json = JsonSerializer.Serialize(films, options);
            System.IO.File.WriteAllText(path, json);
        }
    }
}
