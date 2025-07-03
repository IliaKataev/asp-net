using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Список книг получен");

            var books = new List<Book>
            {
                new("Куколки", "Джон Уиндем"),
                new("Чума", "Альбер Камю")
            };

            return Ok(books);
        }

        [HttpPost("order")]
        public IActionResult Order([FromBody]OrderModel order)
        {
            if(order.Items.Count == 0)
            {
                return BadRequest("Пустой заказ");
            }

            _logger.LogInformation("Заказ оформлен: {@Items}", order.Items);
            return Ok("Заказ принят");
        }
    }
}
