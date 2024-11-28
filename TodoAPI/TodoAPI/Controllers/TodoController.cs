using Microsoft.AspNetCore.Mvc;
using TodoAPI.Data;
using TodoAPI.Dto;
using TodoAPI.Services.Interfaces;

namespace TodoAPI.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _context;
        private readonly ITodoService _todoService;
        public TodoController(TodoDbContext context, ITodoService todoService)
        {
            _context = context;
            _todoService = todoService;
        }

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetSpecificTodo([FromRoute] int id)
        {
            var todo = await _todoService.GetTodo(id);
            return Ok(todo);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = await _todoService.GetAllTodos();
            return Ok(todos);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoCreateDto dto)
        {
            await _todoService.CreateTodo(dto);
            return Ok("Created successful");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo([FromBody] TodoUpdateDto dto, [FromRoute] int id)
        {
            await _todoService.UpdateTodo(dto, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id)
        {
            await _todoService.DeleteTodo(id);
            return NoContent();
        } 
    }
}
