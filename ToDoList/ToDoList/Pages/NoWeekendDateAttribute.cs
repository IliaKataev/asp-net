using System.ComponentModel.DataAnnotations;

namespace ToDoList.Pages
{
    public class NoWeekendDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value is DateTime date)
            {
                return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
            }

            return true;
        }
    }
}
