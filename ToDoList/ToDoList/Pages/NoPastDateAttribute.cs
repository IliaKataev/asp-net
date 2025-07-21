using System.ComponentModel.DataAnnotations;


namespace ToDoList.Pages
{
    public class NoPastDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date.Date >= DateTime.Today;
            }

            return true;
        }
    }

}
