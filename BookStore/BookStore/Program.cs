using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Text.Json;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            //2 ������
            builder.Services.Configure<Models.StoreOptions>(builder.Configuration.GetSection("StoreOptions"));

            //3 �����������
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            //4 ���� � ������
            builder.Services.AddSession();
            builder.Services.AddDistributedMemoryCache();

            //7 WEB API
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var allOrders = new List<OrderModel>();

            var app = builder.Build();

            app.UseDefaultFiles();


            //5 ��������� ������
            app.UseExceptionHandler("/error");
            app.UseStatusCodePagesWithReExecute("/status/{0}");

            //1 ���� �����
            app.UseStaticFiles();
            app.UseRouting();

            //4 ������
            app.UseSession();

            //6 Results API
            app.MapGet("/api/books", () =>
            {
                var books = new List<Book>
                {
                    new("����� � ���", "�.�. �������"),
                    new("������ � ���������", "�. �. ��������"),
                    new("������� ������", "�. �. ������"),
                };

                return Results.Json(books);
            });

            app.MapPost("api/order", async (HttpContext context) =>
            {
                using var reader = new StreamReader(context.Request.Body);
                var body = await reader.ReadToEndAsync();

                var order = JsonSerializer.Deserialize<OrderModel>(body);
                if (order == null || order.Items.Count == 0)
                    return Results.BadRequest("������ ����� (((");

                var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("Orders");
                logger.LogInformation("������ ����� �� {Count} ����", order.Items.Count);

                context.Session.SetString("LastOrder", JsonSerializer.Serialize(order));

                allOrders.Add(order);

                return Results.Redirect("/thanks.html");
            });

            app.MapGet("/api/orders", () =>
            {
                return Results.Json(allOrders);
            });

            app.MapGet("api/last-order", (HttpContext context) =>
            {
                var json = context.Session.GetString("LastOrder");
                return json != null ? Results.Json(JsonSerializer.Deserialize<OrderModel>(json)) : Results.NotFound();
            });

            app.Map("/error", () => Results.Problem("�� ���, ��������� ������"));
            app.Map("/status/{code}", (int code) => Results.Text($"������ {code}"));

            app.MapGet("/manual", () =>
            {
                var filepath = "wwwroot/manual.pdf";
                return Results.File(filepath, "application/pdf", "instruction.pdf");
            });
                
               

            app.MapGet("/welcome", () => Results.Text("����� ���������� � ��������"));

            app.MapControllers();

            app.Run();
        }
    }
}
