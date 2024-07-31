using FluentValidation;
using TaskManager.Console.EFCore;
using TaskManager.REST.Api.Tickets;

var assembly = AppDomain.CurrentDomain.Load("TaskManager.Mediatr");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddDbContext<TaskManagerDbContext>();
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseCors();
}

app.UseHttpsRedirection();

app.MapGetAllTickets();

app.Run();
