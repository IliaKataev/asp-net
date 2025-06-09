using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ninject;
using IoC_DI.SongDirectory;
using IoC_DI.SongDirectory.models;
using IoC_DI.SongDirectory.output;

namespace IoC_DI.Pages
{
    public class SongModelModel : PageModel
    {
        public string Result { get; set; } = string.Empty;
        public void OnGet()
        {
            var kernel = new StandardKernel(new SongModule());

            var song = kernel.Get<ISong>();
            var formatter = kernel.Get<IFormatter>();
            var output = kernel.Get<IOutput>();

            string raw = song.GetFullInfo();
            string form = formatter.Format(raw);

            Result = form;
            output.Write(form);

        }
    }
}
