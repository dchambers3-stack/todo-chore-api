using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoChoreApp2.Models;
using TodoChoreApp2.Models.Dtos;

namespace TodoChoreApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _context;
        public TodoController(TodoDbContext context)
        {
            _context = context;
        }

        [HttpGet("todo-items")]
        public async Task<IEnumerable<TodoDto>> GetTodoList()
        {
            var items = await _context.TodoItems
                 .Select(item => new TodoDto
                 {
                     Id = item.Id,
                     PublicId = item.PublicId,
                     Title = item.Title,
                     IsCompleted = item.IsCompleted
                 })
                 .ToListAsync();
            return items;
        }
        [HttpPost("todo-item")]
        public async Task<IActionResult> CreateTodoItem([FromBody] TodoDto todoDto)
        {
            var todoItem = new TodoItem
            {
                PublicId = Guid.NewGuid(),
                Title = todoDto.Title ?? string.Empty,
                IsCompleted = false
            };
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return Ok(new TodoDto
            {
                Id = todoItem.Id,
                PublicId = todoItem.PublicId,
                Title = todoItem.Title,
                IsCompleted = todoItem.IsCompleted
            });
        }

        [HttpPatch("todo-items/{publicId}/toggle")]
        public async Task<IActionResult> ToggleTodoItemCompletion([FromRoute] Guid publicId)
        {
            var todoItem = await _context.TodoItems
                .FirstOrDefaultAsync(item => item.PublicId == publicId);
            if (todoItem == null)
            {
                return NotFound(new { Message = $"Todo item with PublicId {publicId} not found." });
            }
            todoItem.IsCompleted = !todoItem.IsCompleted;
            await _context.SaveChangesAsync();
            return Ok(new TodoDto
            {
                Id = todoItem.Id,
                PublicId = todoItem.PublicId,
                Title = todoItem.Title,
                IsCompleted = todoItem.IsCompleted
            });

        }
        [HttpDelete("todo-item/{publicId}")]
        public async Task<IActionResult> RemoveChore([FromRoute] Guid publicId)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(item => item.PublicId == publicId);
            if (item == null)
            {
                return NotFound(new { Message = $"Cannot find todo item." });


            }
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
            return Ok("Success!");
        }
    }
}

      