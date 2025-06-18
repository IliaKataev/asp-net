using Kino_Pesochnisa_16._06._25.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kino_Pesochnisa_16._06._25.Pages
{
    public class FilmModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        [FromRoute]
        public string? Style { get; set; }

        [BindProperty]
        public IWatch NewFilm { get; set; } = new();


        public List<IWatch> Films { get; set; } = [];
        public void OnGet()
        {
            var films = GetAllFilms();

            if (!string.IsNullOrEmpty(Style))
                Films = films.Where(f => f.Style == Style).ToList();
            else
                Films = films;
        }

        public void OnPost()
        {
            Films = GetAllFilms();

            if (!string.IsNullOrEmpty(NewFilm.Name))
            {
                Films.Add(NewFilm);
            }
        }

        public IActionResult OnPostSave()
        {
            var films = string.Join('\n', GetAllFilms().Select(f => f.ToString()));
            var bytes = System.Text.Encoding.UTF8.GetBytes(films);
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

        private List<IWatch> GetAllFilms() => new()
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

        
    }
}
