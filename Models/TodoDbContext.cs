using Microsoft.EntityFrameworkCore;

namespace TodoChoreApp2.Models
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }

        // DbSet for Todo items
        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}
