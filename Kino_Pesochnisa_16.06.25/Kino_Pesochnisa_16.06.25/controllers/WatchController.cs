using Microsoft.AspNetCore.Mvc;
using Kino_Pesochnisa_16._06._25.models;

namespace Kino_Pesochnisa_16._06._25.controllers
{
    public class WatchController : Controller
    {
        private static List<IWatch> Films = new List<IWatch>
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

        public IActionResult Index()
        {
            return View(Films);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
