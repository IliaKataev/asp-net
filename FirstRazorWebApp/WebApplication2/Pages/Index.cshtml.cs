using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IConfiguration configuration;
        public string CurrentDateTime { get; set; }

        public string? MessageToMe { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            configuration = config;
        }

        public void OnGet()
        {
            CurrentDateTime = DateTime.Now.ToString("f");
            MessageToMe = configuration["MessageToYou"];
        }
    }
}
