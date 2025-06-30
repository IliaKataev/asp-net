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
                    "�������-���� 12:00",
                    "����� 14:00",
                    "������������� ��������� 23:00"
                };
            }
        }
    }
}
