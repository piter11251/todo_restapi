using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using TodoAPI.Data;
using TodoAPI.Dto;
using TodoAPI.Entities;
using TodoAPI.Services.Interfaces;

namespace TodoAPI.Services
{
    public class TodoService: ITodoService
    {
        private readonly TodoDbContext _context;
        private readonly IMapper _mapper;
        public TodoService(TodoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateTodo(TodoCreateDto dto)
        {
            var newTodo = new Todo
            {
                Title = dto.Title,
                Description = dto.Description,
                Expiry = dto.Expiry
            };

            await _context.AddAsync(newTodo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTodo(TodoUpdateDto dto, int id)
        {
            var todo = await _context.Todos
                .FirstOrDefaultAsync(t => t.Id == id);

            if(todo == null)
            {
                throw new Exception("Not found");
            }

            todo.Title = dto.Title;
            todo.Description = dto.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTodo(int id)
        {
            var todo = await _context.Todos
                .FirstOrDefaultAsync(t => t.Id == id);

            if(todo == null)
            {
                throw new Exception("Not found");
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoCreateDto> GetTodo(int id)
        {
            var todo = await _context.Todos.
                FirstOrDefaultAsync(t => t.Id == id);

            if(todo == null)
            {
                throw new Exception("Not found");
            }

            var todoDto = _mapper.Map<TodoCreateDto>(todo);

            return todoDto;
        }

        public async Task<List<TodoCreateDto>> GetAllTodos()
        {
            var todos = await _context.Todos.ToListAsync();
            var todosDto = new List<TodoCreateDto>();
            if(todos == null)
            {
                throw new Exception("Not found");
            }

            foreach(var todo in todos)
            {
                var newTodo = _mapper.Map<TodoCreateDto>(todo);
                todosDto.Add(newTodo);
            }

            return todosDto;
        }
    }
}
