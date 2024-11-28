using TodoAPI.Dto;
using TodoAPI.Dto.Validators;

namespace TodoAPI.Services.Interfaces
{
    public interface ITodoService
    {
        Task CreateTodo(TodoCreateDto dto);
        Task UpdateTodo(TodoUpdateDto dto, int id);
        Task DeleteTodo(int id);
        Task<List<TodoCreateDto>> GetAllTodos();
        Task<TodoCreateDto> GetTodo(int id);
    }
}
