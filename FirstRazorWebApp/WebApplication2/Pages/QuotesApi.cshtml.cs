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
                ("Вы рождены, чтобы быть чьей-то женой. Так почему бы не моей?", "Маргарет Митчелл"),
                ("Прогресс неизбежен, его прекращение означало бы гибель цивилизации.", "Андрей Дмитриевич Сахаров"),
                ("Дело не в дороге, которую мы выбираем, а в том, что внутри нас заставляет нас выбирать дорогу.", "Боб Тидбол"),
            };

            var rand = new Random();
            var quo = quotes[rand.Next(quotes.Length)];


            return new JsonResult(new { quote = quo.text, author = quo.author });
        }
    }
}
