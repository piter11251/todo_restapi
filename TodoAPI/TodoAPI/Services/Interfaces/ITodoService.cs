using TodoAPI.Dto;
using TodoAPI.Dto.Validators;
using TodoAPI.Entities;

namespace TodoAPI.Services.Interfaces
{
    public interface ITodoService
    {
        Task CreateTodo(TodoCreateDto dto);
        Task UpdateTodo(TodoUpdateDto dto, int id);
        Task DeleteTodo(int id);
        Task<List<TodoCreateDto>> GetAllTodos();
        Task<TodoCreateDto> GetTodo(int id);
        Task<TodoListsDto> IncomingTodo();
        Task MakeTodoAsDone(int id, string status);
        Task SetPercentCompleted(int percentCompleted, int id);
    }
}
