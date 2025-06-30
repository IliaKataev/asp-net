using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CinemaTheater.Pages
{
    public class ShowtimesModel : PageModel
    {
        public string Cinema { get; set; }
        public DateTime? DateTime { get; set; }

        public List<string> Movies { get; set; } = new();

        public void OnGet(string cinema, DateTime? dateTime)
        {
            this.Cinema = cinema;
            this.DateTime = dateTime;

            if(DateTime.HasValue)
            {
                Movies = new List<string>
                {
                    "Человек-Паук 12:00",
                    "Акира 14:00",
                    "Омерзительная восьмерка 23:00"
                };
            }
        }
    }
}
