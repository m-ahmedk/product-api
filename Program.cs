using Microsoft.EntityFrameworkCore;
using ProductDemo.Data;
using ProductDemo.Extensions;
using ProductDemo.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

LogConfigurator.ConfigureLogging(builder.Configuration);
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register DB Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"{connectionString}"));

// Register Swagger Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Repositories, Services, Validators and Mappings via custom made Service Extension
builder.Services.AddProjectRepositories();
builder.Services.AddProjectServices();
builder.Services.AddProjectValidators();
builder.Services.AddProjectMappings();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Use Swagger middleware here
app.UseSwagger();
app.UseSwaggerUI(); // This enables browser testing

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
