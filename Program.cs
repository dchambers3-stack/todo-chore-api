using Microsoft.EntityFrameworkCore;
using TodoChoreApp2.Models;

namespace TodoChoreApp2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure SQLite database for TodoDbContext to match MyBudgetApplication.
            // Connection string is read in this order:
            // 1. ConnectionStrings:DefaultConnection from configuration (appsettings/user secrets)
            // 2. Environment variable TODO_CONNECTION
            // 3. Environment variable DEFAULT_CONNECTION
            // 4. Fallback to a local SQLite file (share with MyBudgetApplication)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? Environment.GetEnvironmentVariable("TODO_CONNECTION")
                                   ?? Environment.GetEnvironmentVariable("DEFAULT_CONNECTION")
                                   ?? "Data Source=budget.db";

            builder.Services.AddDbContext<TodoDbContext>(options =>
                options.UseSqlite(connectionString));

            // Add CORS - Allow all
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // CORS must be first
            app.UseCors("AllowAll");

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
