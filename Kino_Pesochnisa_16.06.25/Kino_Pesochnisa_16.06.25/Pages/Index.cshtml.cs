using Kino_Pesochnisa_16._06._25.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Kino_Pesochnisa_16._06._25.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly string path;

        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            path = Path.Combine(environment.ContentRootPath, "AppData", "films.json");
        }

        public List<IWatch> Films { get; set; } = [];

        public void OnGet()
        {
            if (System.IO.File.Exists(path))
            {
                var json = System.IO.File.ReadAllText(path);
                Films = JsonSerializer.Deserialize<List<IWatch>>(json) ?? new();
            }
        }
    }
}
