using System.ComponentModel.DataAnnotations;
using ToDoList.Pages;
namespace ToDoList.Model
{
    public class TaskModel
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // sdfgjhkdfjkhshdkjfhbdsf


        [Required(ErrorMessage = "Введите заголовок")]
        [StringLength(150)]
        public string Title { get; set; }


        [Range(typeof(DateTime), "01/01/2020", "01/01/2030", ErrorMessage = "Веберите год между 20 и 30")]
        [NoWeekendDate(ErrorMessage = "Выберите будние дни")]
        public DateTime DueDate { get; set; } = DateTime.Now;
    }
}
