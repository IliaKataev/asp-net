namespace CinemaTheater
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}


//Сделать RazorPage, которая будет показывать расписание кинотеатра по маршруту:

// "/showtimes/{cinema}/{date?}"

// список фильмов должен быть в list<string>
// "Человек-Паук 12:00"
// "Акира 14:00"
// "Омерзительная восьмерка 23:00"

//если даты нет, то вместо расписания показывать текст: "Пожалуйста, укажите дату в маршруте"