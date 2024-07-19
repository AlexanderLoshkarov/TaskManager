using Microsoft.EntityFrameworkCore;
using System.Threading;
using TaskManager.Console.EFCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseCors();
}

app.UseHttpsRedirection();

app.MapGet("/tickets", async () =>
{
  using var _dbContext = new TaskManagerDbContext();
  var tickets = await _dbContext.Tickets.ToArrayAsync();

  var ticketsResponse = tickets.Select(ticket => new TicketResponse(
    ticket.Id, 
    ticket.Title, 
    ticket.Description, 
    ticket.TicketStatus, 
    ticket.CreatedAt, 
    ticket.User?.UserName)
  ).ToArray();

  return ticketsResponse;
});

app.Run();

public record TicketResponse(
  string Id, 
  string Title, 
  string? Description, 
  TicketStatus TicketStatus, 
  DateTime CreatedAt, 
  string? UserName
);
