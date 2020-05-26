using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearningCore.Data;
using LearningCore.Service;
using LearningCore.Common.Extentions;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace LearningCore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly LearningCoreContext _context;

        public ILogger<TodoItemsController> _logger { get; }

        public TodoItemsController(LearningCoreContext context
            ,ILogger<TodoItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }
        /// <summary>
        /// 根据Id获取Todo数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/TodoItems/5
        [HttpGet("{id}.{format?}")]
        [FormatFilter]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(TodoItem), StatusCodes.Status200OK)]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
           
            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions),
                     nameof(DefaultApiConventions.Put))]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            if (!TryValidateModel(todoItem, nameof(todoItem)))//修改后重新验证
                return BadRequest(JsonSerializer.Serialize(ModelState.Values.Where(x => x.Errors.Count > 0).Select(x => new { key = x.GetKeyValue("Key"), x.RawValue, x.Errors.First().ErrorMessage }).ToList()));
            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                  
                    throw;//new Exception("");
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]     // Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  // BadRequest
        public async Task<IActionResult> PostTodoItem(TodoItem todoItem)//Task<ActionResult<TodoItem>>
        {
            if (!ModelState.IsValid)
                return 
                  new JsonResult( ModelState.Values.Where(x => x.Errors.Count > 0).Select(x => new { key = x.GetKeyValue("Key"), x.RawValue, x.Errors.First().ErrorMessage }).ToList())
                    ;
            try
            {
                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;// new Exception(ex.Message);
            }

            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
