using FluentValidation;
using FluentValidation.AspNetCore;
using TodoAPI.Data;
using TodoAPI.Dto;
using TodoAPI.Dto.Mapper;
using TodoAPI.Dto.Validators;
using TodoAPI.Services;
using TodoAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Automapper for Todo DTOs
builder.Services.AddAutoMapper(typeof(TodoMapProfile));

// Todo services
builder.Services.AddScoped<ITodoService, TodoService>();

// Added Fluent Validator
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Registered DbContext
builder.Services.AddDbContext<TodoDbContext>();

// Registered Fluent Validator
builder.Services.AddScoped<IValidator<TodoCreateDto>, TodoCreateValidator>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
