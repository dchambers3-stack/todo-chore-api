using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TodoChoreApp2.Models
{
    // Design-time factory to provide DbContextOptions when running EF tools
    public class TodoDbContextFactory : IDesignTimeDbContextFactory<TodoDbContext>
    {
        public TodoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TodoDbContext>();

            // Prefer configuration via environment variable or connection string named DefaultConnection
            var connectionString = Environment.GetEnvironmentVariable("TODO_CONNECTION")
                                   ?? Environment.GetEnvironmentVariable("DEFAULT_CONNECTION")
                                   ?? "Data Source=budget.db";

            builder.UseSqlite(connectionString);

            return new TodoDbContext(builder.Options);
        }
    }
}
