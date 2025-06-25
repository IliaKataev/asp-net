using Kino_Pesochnisa_16._06._25.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Kino_Pesochnisa_16._06._25.Pages
{
    public class FilmModel : PageModel
    {
        private readonly string path;
        private readonly string imagePath;

        public FilmModel(IWebHostEnvironment webHostEnvironment)
        {
            path = Path.Combine(webHostEnvironment.ContentRootPath, "AppData", "films.json");
            imagePath = Path.Combine(webHostEnvironment.WebRootPath, "images");
            CheckFileExist();
        }



        [BindProperty(SupportsGet = true)]
        public string? Style { get; set; }

        [BindProperty]
        public IWatch NewFilm { get; set; } = new();

        [BindProperty]
        public IFormFile? UploadedImage { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchAuthor { get; set; }

        public List<string> AvailableStyles { get; set; } = new();


        public List<IWatch> Films { get; set; } = [];
        public void OnGet()
        {
            var films = ReadFromFile();

            AvailableStyles = films
                .Select(f => f.Style)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct()
                .OrderBy(s => s)
                .ToList();

            if (!string.IsNullOrEmpty(Style))
                films = films.Where(f => f.Style.Contains(Style, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!string.IsNullOrEmpty(SearchName))
                films = films.Where(f => f.Name.Contains(SearchName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!string.IsNullOrEmpty(SearchAuthor))
                films = films.Where(f => f.Author.Contains(SearchAuthor, StringComparison.OrdinalIgnoreCase)).ToList();
            Films = films;
        }

        public IActionResult OnPost()
        {
            var films = ReadFromFile();

            if (!string.IsNullOrEmpty(NewFilm.Name))
            {
                NewFilm.Id = films.Any() ? films.Max(f => f.Id) + 1 : 1;

                if (UploadedImage != null && UploadedImage.Length > 0)
                {
                    var filename = $"{Guid.NewGuid()}{Path.GetExtension(UploadedImage.FileName)}";
                    var filePath = Path.Combine(imagePath, filename);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        UploadedImage.CopyTo(stream);
                    }

                    NewFilm.ImagePath = $"/images/{filename}";
                }
                else
                {
                    NewFilm.ImagePath = $"/images/dragonBallZ.jpg";
                }


                films.Add(NewFilm);
                SaveToFile(films); ///// сделать так, чтобы уже существующие фильмы не записывались повторно

                NewFilm = new IWatch();
            }

            Films = films;

            return RedirectToPage();
        }

        public IActionResult OnPostSave()
        {
            var films = ReadFromFile();
            var text = string.Join('\n', films.Select(f => f.ToString()));
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            return File(bytes, "text/plain", "films.txt");

        }

        public IActionResult OnPostErrorTest()
        {
            return StatusCode(418, "I'm a teapot");
        }

        public IActionResult OnPostRedirect()
        {
            return RedirectToPage("Privacy");
        }

        private void CheckFileExist()
        {
            if(!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            if(!System.IO.File.Exists(path))
            {
                var defaultFilms = new List<IWatch>
                {
                    new IWatch
                    {
                            Id = 0,
                            Name = "Киборг убийца",
                            Author = "Джеймс Камерон",
                            Style = "Триллер",
                            Description = "Ну а что тут объяснять?",
                            Seances = new List<string> {
                                "10:00",
                                "15:00",
                                "23:47"
                            },
                            ImagePath = "/images/imageImage.jpg"
                     },
                    new IWatch
                    {
                        Id = 1,
                        Name = "Чебурашка",
                        Author = "Союзмультфильм",
                        Style = "Комедия",
                        Description = "Популярно в новый год",
                        Seances = new List<string> {
                            "02:00",
                            "12:00",
                            "17:35"
                        },
                        ImagePath = "/images/dragonBallZ.jpg"
                    },
                    new IWatch
                    {   
                        Id = 2,
                        Name = "Мой сосед Тоторо",
                        Author = "Хаяо Миядзаки",
                        Style = "Анимация",
                        Description = "Пупупу",
                        Seances = new List<string> {
                            "07:00",
                            "11:21",
                            "22:17"
                        },
                        ImagePath = "/images/tenet.jpg"
                    }
                };
                SaveToFile(defaultFilms);
            }

            if (!Directory.Exists(imagePath))
                Directory.CreateDirectory(imagePath);
        }

        private List<IWatch> ReadFromFile()
        {
            if (!System.IO.File.Exists(path))
                return new List<IWatch>();

            var json = System.IO.File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(json))
                return new List<IWatch>();

            try
            {
                var result = JsonSerializer.Deserialize<List<IWatch>>(json);
                return result ?? new();
            }
            catch (JsonException)
            {
                CheckFileExist();
                return ReadFromFile();
            }

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
