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
builder.Services.AddAutoMapper(typeof(TodoMapProfile));
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddDbContext<TodoDbContext>();
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
