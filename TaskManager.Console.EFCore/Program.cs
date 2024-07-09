using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TaskManager.Console.EFCore;

using (var context = new TaskManagerDbContext())
{
  context.Database.EnsureCreated();
  Console.WriteLine("Database created and seeded successfully.");
}