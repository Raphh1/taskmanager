using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerRaph.Models;

namespace TaskManagerRaph.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskContext _taskContext;

        public TaskController(TaskContext taskContext)
        {
            _taskContext = taskContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tache>>> GetTasks()
        {
            var tasks = await _taskContext.Tasks.ToListAsync();
            if (tasks == null)
            {
                return NotFound();
            }
            return tasks;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tache>> GetTask(int id)
        {
            var task = await _taskContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        [HttpPost]
        public async Task<ActionResult<Tache>> PostTask(Tache task)
        {
            _taskContext.Tasks.Add(task);
            await _taskContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, Tache task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _taskContext.Entry(task).State = EntityState.Modified;

            try
            {
                await _taskContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _taskContext.Tasks.Remove(task);
            await _taskContext.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _taskContext.Tasks.Any(e => e.Id == id);
        }
    }
}
