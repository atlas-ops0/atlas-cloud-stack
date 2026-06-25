
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Api.Data;
using TaskTracker.Api.Models;


namespace TaskTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TodoItemsController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
    {
        return await _context.TodoItems.ToListAsync();
    }
    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(TodoItem todoItem)
    {
        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = todoItem.Id }, todoItem);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetById(int id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return todoItem;

    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TodoItem todoItem)
    {
        if (id != todoItem.Id)
        {
            return BadRequest();
        }
        _context.Entry(todoItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }
        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
