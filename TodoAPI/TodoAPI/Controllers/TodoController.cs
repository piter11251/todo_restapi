using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Dto;
using TodoAPI.Entities.Enums;
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

        // Endpoint for incoming todos(today, this week, this month)

        [HttpGet("incoming")]
        public async Task<IActionResult> GetIncomingTodo()
        {
            var todoList = await _todoService.IncomingTodo();
            return Ok(todoList);
        }

        // Get todo by id

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetSpecificTodo([FromRoute] int id)
        {
            var todo = await _todoService.GetTodo(id);
            return Ok(todo);
        }

        //Get All Todos

        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = await _todoService.GetAllTodos();
            return Ok(todos);
        }

        // Create todo with TodoCreateDto

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] TodoCreateDto dto)
        {
            await _todoService.CreateTodo(dto);
            return Ok("Created successful");
        }

        // Update specified todo with TodoUpdateDto

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo([FromBody] TodoUpdateDto dto, [FromRoute] int id)
        {
            await _todoService.UpdateTodo(dto, id);
            return Ok();
        }

        // Delete todo with specified id

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id)
        {
            await _todoService.DeleteTodo(id);
            return NoContent();
        }

        // Change todo status of specified id

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> MarkTodoAsDone([FromQuery] string todoStatus, [FromRoute] int id)
        {
            await _todoService.MakeTodoAsDone(id, todoStatus);
            return Ok();
        }

        // Set todo percent completed of specified id

        [HttpPatch("{id}")]
        public async Task<IActionResult> SetPercentCompleted([FromQuery] int percentCompleted, [FromRoute] int id)
        {
            await _todoService.SetPercentCompleted(percentCompleted, id);
            return Ok();
        }
    }
}
