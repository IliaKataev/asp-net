using System.ComponentModel.DataAnnotations;
using ToDoList.Pages;
namespace ToDoList.Model
{
    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }
    public class TaskModel
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // sdfgjhkdfjkhshdkjfhbdsf


        [Required(ErrorMessage = "Введите заголовок")]
        [StringLength(150)]
        public string Title { get; set; }


        [Range(typeof(DateTime), "01/01/2020", "01/01/2030", ErrorMessage = "Веберите год между 20 и 30")]
        [NoWeekendDate(ErrorMessage = "Выберите будние дни")]
        [NoPastDate(ErrorMessage = "Дата не может быть меньше текущей")]
        public DateTime DueDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Выберите приоритет")]
        public TaskPriority Priority { get; set; } = TaskPriority.Low;

        public bool IsCompleted { get; set; } = false;

        public Tag Tag { get; set; }
    }
}
