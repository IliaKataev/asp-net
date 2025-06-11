using Microsoft.AspNetCore.Mvc;
using CarApplication.models;
using System.Text.RegularExpressions;

namespace CarApplication.controllers
{
    public class CarController : Controller
    {
        private static List<Car> Cars = new List<Car>
        {
            new Car
            {
                Name = "Land Cruiser 200",
                Color = "White",
                Manufacturer = "Toyota",
                Year = 2018,
                EngineVolume = 4.4
            }
        };

        public IActionResult Index()
        {
            return View(Cars);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                Cars.Add(car);
                return RedirectToAction("Index");
            }
            return View(car);
        }

        public IActionResult SaveToFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "cars.txt");
            var lines = Cars.Select(c => c.ToString());
            System.IO.File.WriteAllLines(path, lines);
            ViewBag.FilePath = path;

            return View(Cars);
        }

        [HttpGet]
        public IActionResult LoadFromFile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoadFromFile(IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    string content = reader.ReadToEnd();

                    var blocks = Regex.Split(content, @"(\r?\n){2,}")
                        .Select(b => b.Trim())
                        .Where(b => !string.IsNullOrWhiteSpace(b))
                        .ToList();

                    foreach (var block in blocks)
                    {
                        var car = ParseCarFromBlock(block);
                        if (car != null)
                            Cars.Add(car);
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.Error = "file ne vibran ili pust";
            return View();
        }

        private Car? ParseCarFromBlock(string block)
        {
            var car = new Car();

            var parts = block.Split(',');

            foreach (var part in parts)
            {
                var kv = part.Split(':', 2);
                if (kv.Length != 2) continue;

                var key = kv[0].Trim().ToLower();
                var value = kv[1].Trim();

                switch (key)
                {
                    case "название":
                        car.Name = value;
                        break;
                    case "цвет":
                        car.Color = value;
                        break;
                    case "производитель":
                        car.Manufacturer = value;
                        break;
                    case "год выпуска":
                        if (int.TryParse(value, out var year))
                            car.Year = year;
                        break;
                    case "объём двигателя":
                        // заменяем запятую на точку — для парсинга
                        if (double.TryParse(value.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var volume))
                            car.EngineVolume = volume;
                        break;
                }
            }

            return string.IsNullOrEmpty(car.Name) ? null : car;
        }


    }
}
