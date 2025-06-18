using Kino_Pesochnisa_16._06._25.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace Kino_Pesochnisa_16._06._25.Pages
{
    public class FilmModel : PageModel
    {
        private readonly string path;

        public FilmModel(IWebHostEnvironment webHostEnvironment)
        {
            path = Path.Combine(webHostEnvironment.ContentRootPath, "AppData", "films.json");
            CheckFileExist();
        }



        [BindProperty(SupportsGet = true)]
        public string? Style { get; set; }

        [BindProperty]
        public IWatch NewFilm { get; set; } = new();


        public List<IWatch> Films { get; set; } = [];
        public void OnGet()
        {
            var films = ReadFromFile();

            if (!string.IsNullOrEmpty(Style))
                Films = films.Where(f => f.Style == Style).ToList();
            else
                Films = films;
        }

        public IActionResult OnPost()
        {
            var films = ReadFromFile();

            if (!string.IsNullOrEmpty(NewFilm.Name))
            {
                NewFilm.Id = films.Any() ? films.Max(f => f.Id) + 1 : 1;
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
                            Name = "Киборг убийца",
                            Author = "Джеймс Камерон",
                            Style = "Триллер",
                            Description = "Ну а что тут объяснять?",
                            Seances = new List<string> {
                                "10:00",
                                "15:00",
                                "23:47"
                            }
                     },
                    new IWatch
                    {
                        Name = "Чебурашка",
                        Author = "Союзмультфильм",
                        Style = "Комедия",
                        Description = "Популярно в новый год",
                        Seances = new List<string> {
                            "02:00",
                            "12:00",
                            "17:35"
                        }
                    },
                    new IWatch
                    {
                        Name = "Мой сосед Тоторо",
                        Author = "Хаяо Миядзаки",
                        Style = "Анимация",
                        Description = "Пупупу",
                        Seances = new List<string> {
                            "07:00",
                            "11:21",
                            "22:17"
                        }
                    }
                };
                SaveToFile(defaultFilms);
            }
        }

        private List<IWatch> ReadFromFile()
        {
            var json = System.IO.File.ReadAllText(path);
            var result = JsonSerializer.Deserialize<List<IWatch>>(json);
            return result ?? new();
        }

        private void SaveToFile(List<IWatch> films)
        {
            var json = JsonSerializer.Serialize(films, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(path, json);
        }

        
    }
}
