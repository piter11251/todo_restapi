using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TodoAPI.Data;
using TodoAPI.Dto;
using TodoAPI.Dto.Mapper;
using TodoAPI.Entities;
using TodoAPI.Services;
using TodoAPI.Services.Interfaces;
using Xunit;

namespace TestTodoApi
{
    public class TodoServiceTest : IDisposable
    {
        private readonly TodoDbContext _context;
        private readonly TodoService _todoService;

        public TodoServiceTest()
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TodoDbContext(options);
            _context.Database.EnsureCreated();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(new TodoMapProfile())).CreateMapper();
            _todoService = new TodoService(_context, mapper);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task CreateTodo_ShouldAddTodoToDatabase()
        {
            var dto = new TodoCreateDto
            {
                Title = "Test Todo",
                Description = "Test Description",
                Expiry = DateTime.UtcNow.AddDays(1)
            };

            await _todoService.CreateTodo(dto);

            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Title == dto.Title);
            Assert.NotNull(todo);
            Assert.Equal(dto.Title, todo.Title);
            Assert.Equal(dto.Description, todo.Description);
            Assert.Equal(dto.Expiry, todo.Expiry);
        }

        [Fact]
        public async Task CreateTodo_ShouldThrowException_WhenTitleIsInvalid()
        {
            var dto = new TodoCreateDto
            {
                Title = "", // Pusty tytuł
                Description = "Test Description",
                Expiry = DateTime.UtcNow.AddDays(1)
            };

            var response = await _client
        }

        [Fact]
        public async Task CreateTodo_ShouldThrowExceptionForTitle()
        {
            var dto = new TodoCreateDto
            {
                Title = "",
                Description = "Test Description",
                Expiry = DateTime.UtcNow.AddDays(1)
            };

            await _todoService.CreateTodo(dto);

            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Title == dto.Title);
            Assert.NotNull(todo);
            Assert.Equal(dto.Title, todo.Title);
            Assert.Equal(dto.Description, todo.Description);
            Assert.Equal(dto.Expiry, todo.Expiry);
        }

        [Fact]
        public async Task UpdateTodo_ShouldUpdateExistingTodo()
        {
            var todo = new Todo { Title = "Old Title", Description = "Old Description", Expiry = DateTime.UtcNow };
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            var dto = new TodoUpdateDto
            {
                Title = "Updated Title",
                Description = "Updated Description"
            };

            await _todoService.UpdateTodo(dto, todo.Id);

            var updatedTodo = await _context.Todos.FindAsync(todo.Id);
            Assert.NotNull(updatedTodo);
            Assert.Equal(dto.Title, updatedTodo.Title);
            Assert.Equal(dto.Description, updatedTodo.Description);
        }

        [Fact]
        public async Task DeleteTodo_ShouldRemoveTodoFromDatabase()
        {
            var todo = new Todo { Title = "Test Title", Description = "Test Description", Expiry = DateTime.UtcNow };
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            await _todoService.DeleteTodo(todo.Id);

            var deletedTodo = await _context.Todos.FindAsync(todo.Id);
            Assert.Null(deletedTodo);
        }

        [Fact]
        public async Task GetTodo_ShouldReturnTodoById()
        {
            var todo = new Todo { Title = "Test Title", Description = "Test Description", Expiry = DateTime.UtcNow };
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            var result = await _todoService.GetTodo(todo.Id);

            Assert.NotNull(result);
            Assert.Equal(todo.Title, result.Title);
            Assert.Equal(todo.Description, result.Description);
        }

        [Fact]
        public async Task GetAllTodos_ShouldReturnAllTodos()
        {
            _context.Todos.Add(new Todo { Title = "Todo 1", Description = "Description 1", Expiry = DateTime.UtcNow });
            _context.Todos.Add(new Todo { Title = "Todo 2", Description = "Description 2", Expiry = DateTime.UtcNow });
            await _context.SaveChangesAsync();

            var result = await _todoService.GetAllTodos();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task SetPercentCompleted_ShouldUpdatePercentComplete()
        {
            var todo = new Todo { Title = "Test Title", Description = "Test Description", Expiry = DateTime.UtcNow };
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            await _todoService.SetPercentCompleted(50, todo.Id);

            var updatedTodo = await _context.Todos.FindAsync(todo.Id);
            Assert.NotNull(updatedTodo);
            Assert.Equal(50, updatedTodo.PercentComplete);
        }
    }
}