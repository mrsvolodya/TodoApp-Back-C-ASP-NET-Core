using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace TodoApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TodoController : ControllerBase
  {
    private static List<TodoItem> _tasks = new List<TodoItem>();

    // GET: /api/todo
    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetTasks()
    {
      return Ok(_tasks);
    }

    // POST: /api/todo
    [HttpPost]
    public ActionResult<TodoItem> AddTask(TodoItem task)
    {
      task.Id = _tasks.Any() ? _tasks.Max(t => t.Id) + 1 : 1; // Присвоєння ID
      _tasks.Add(task);
      return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
    }

    // DELETE: /api/todo/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
      var task = _tasks.FirstOrDefault(t => t.Id == id);
      if (task == null)
      {
        return NotFound();
      }

      _tasks.Remove(task);
      return NoContent();
    }

    // The "completed" status is not saved persistently, so it resets after a page reload. 
    // To persist changes, implement storage (e.g., a database or file system).
    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, TodoItem task)
    {
      var existingTask = _tasks.FirstOrDefault(t => t.Id == id);
      if (existingTask == null)
      {
        return NotFound();
      }

      existingTask.IsCompleted = task.IsCompleted;

      return Ok(existingTask);
    }
  }
}
