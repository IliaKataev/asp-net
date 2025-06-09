using IoC_DI.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ninject;

namespace IoC_DI.Pages
{
    public class DrinkModelModel : PageModel
    {
        public string Message { get; set; } = "";
        public void OnGet()
        {
            IKernel kernel = new StandardKernel(new DrinkModule());

            var drink = kernel.Get<IDrink>();
            var writer = kernel.Get<IOutputWriter>();

            Message = drink.GetInfo();
            writer.Write(Message);
        }
    }
}
