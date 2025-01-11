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
    private static List<TodoItem> _tasks = new List<TodoItem>
        {
            new TodoItem { Id = 1, Title = "Learn ASP.NET Core", IsCompleted = false },
            new TodoItem { Id = 2, Title = "Build an API", IsCompleted = false }
        };

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
      task.Id = _tasks.Max(t => t.Id) + 1;
      _tasks.Add(task);
      return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
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

    // PATCH: /api/todo/{id}/complete
    [HttpPatch("{id}/complete")]
    public IActionResult UpdateTaskCompletion(int id)
    {
      var task = _tasks.FirstOrDefault(t => t.Id == id);
      if (task == null)
      {
        return NotFound();
      }

      task.IsCompleted = !task.IsCompleted;
      return Ok(task);
    }

    // GET: /api/todo/{id}
    [HttpGet("{id}")]
    public ActionResult<TodoItem> GetTaskById(int id)
    {
      var task = _tasks.FirstOrDefault(t => t.Id == id);
      if (task == null)
      {
        return NotFound();
      }

      return Ok(task);
    }
  }
}
