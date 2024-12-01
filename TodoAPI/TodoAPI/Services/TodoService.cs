using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using TodoAPI.Data;
using TodoAPI.Dto;
using TodoAPI.Entities;
using TodoAPI.Services.Interfaces;
using System.Linq;
using TodoAPI.Entities.Enums;
using TodoAPI.Dto.Validators;

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

        // In this method object of class TodoListsDto is storing list of today todos, this week todos and this month todos.

        public async Task<TodoListsDto> IncomingTodo()
        {
            var currentDate = DateTime.UtcNow;
            var todoList = await _context.Todos.ToListAsync();
            var todayList = await TodayTodos(todoList, currentDate);
            var thisWeekList = await ThisWeekTodos(todoList, currentDate);
            var thisMonthList = await ThisMonthTodos(todoList, currentDate);
            return new TodoListsDto
            {
                TodayList = todayList,
                ThisWeekList = thisWeekList,
                ThisMonthList = thisMonthList
            };

        }

        // This method changes todo status

        public async Task MakeTodoAsDone(int id, string todoStatus)
        {
            if (!Enum.TryParse<TodoStatus>(todoStatus, true, out var taskStatus))
            {
                throw new Exception($"Invalid status value: {todoStatus}");
            }
            var todo = await _context.Todos.
                FirstOrDefaultAsync(t => t.Id == id);

            if (todo == null)
            {
                throw new Exception("Not found");
            }

            todo.Status = taskStatus;

            await _context.SaveChangesAsync();
        }

        // This method sets todo percent completed

        public async Task SetPercentCompleted(int percentCompleted, int id)
        {
            if(percentCompleted < 0 || percentCompleted > 100)
            {
                throw new Exception("Use value in range 0-100");
            }
            var todo = await _context.Todos
                .FirstOrDefaultAsync(t => t.Id == id);

            if(todo == null)
            {
                throw new Exception("Todo with specified id doesn't exist");
            }

            todo.PercentComplete = percentCompleted;
            await _context.SaveChangesAsync();
        }

        private async Task<List<Todo>> TodayTodos(List<Todo> todoList, DateTime currentDate)
        {
            var todayTodos = await _context.Todos
                .Where(t => t.Expiry.Date == currentDate.Date && currentDate <=  t.Expiry)
                .ToListAsync();
            
            return todayTodos;
        }

        private async Task<List<Todo>> ThisWeekTodos(List<Todo> todoList, DateTime currentDate)
        {
            
            var endOfWeek = currentDate.AddDays(7 - (int)currentDate.DayOfWeek);
            var weekTodos = await _context.Todos
                .Where(t => t.Expiry >= currentDate && t.Expiry <= endOfWeek)
                .ToListAsync();
            return weekTodos;
            
        }

        private async Task<List<Todo>> ThisMonthTodos(List<Todo> todoList, DateTime currentDate)
        {
            var numberOfMonthDays = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            var endOfMonth = currentDate.AddDays(numberOfMonthDays - currentDate.Day);
            var monthTodos = await _context.Todos
                .Where(t => t.Expiry >= currentDate && t.Expiry < endOfMonth)
                .ToListAsync();
            return monthTodos;
        }


    }
}
