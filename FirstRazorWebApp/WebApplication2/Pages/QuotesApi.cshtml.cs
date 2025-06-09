using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication2.Pages
{
    public class QuotesApiModel : PageModel
    {
        public JsonResult OnGet()
        {
            var quotes = new (string text, string author)[]
            {
                ("�� �������, ����� ���� ����-�� �����. ��� ������ �� �� ����?", "�������� �������"),
                ("�������� ���������, ��� ����������� �������� �� ������ �����������.", "������ ���������� �������"),
                ("���� �� � ������, ������� �� ��������, � � ���, ��� ������ ��� ���������� ��� �������� ������.", "��� ������"),
            };

            var rand = new Random();
            var quo = quotes[rand.Next(quotes.Length)];


            return new JsonResult(new { quote = quo.text, author = quo.author });
        }
    }
}
