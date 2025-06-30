using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design.Serialization;

namespace Kino_Pesochnisa_16._06._25.Pages
{
    public class ProductModelModel : PageModel
    {
        public int id { get; set; }
        public void OnGet(int id)
        {
            this.id = id;
        }
    }
}
