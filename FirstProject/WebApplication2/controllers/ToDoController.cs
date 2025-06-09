using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using WebApplication2.models;

namespace WebApplication2.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private static List<TodoItem> items = new();

        [HttpGet]
        public IActionResult Get()
        {
            var items = new List<TodoItem>
            {
                new TodoItem {Id = 1, Name = "first", IsCompleted = false},
                new TodoItem {Id = 2, Name = "second", IsCompleted = true},
                new TodoItem {Id = 3, Name = "third", IsCompleted = false},
            };

            return Ok(items);
        }

        [HttpPost]
        public IActionResult Create(TodoItem item)
        {
            item.Id = items.Count + 1;
            items.Add(item);
            return CreatedAtAction(nameof(Get), new {id = item.Id} , item);
        }
    }
}
