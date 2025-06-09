namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            // builder.Services.Add

            var app = builder.Build();

            app.MapControllers();


            //app.MapGet("/", () => "Hello Worlddfgdfg!");

            app.Run();
        }
    }
}
