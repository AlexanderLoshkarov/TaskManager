using Microsoft.EntityFrameworkCore;

namespace TaskManager.Console.EFCore
{
  public class TaskManagerDbContext : DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      //TODO: Get From Configuration
      optionsBuilder.UseCosmos(
        "https://localhost:8081",
        "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
        "TaskManager");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().HasKey(u => u.Id);

      modelBuilder.Entity<Ticket>().HasKey(t => t.Id);

      modelBuilder.Entity<Ticket>().HasOne(t => t.User)
          .WithMany()
          .HasForeignKey(t => t.UserId);

      var adminId = Guid.NewGuid().ToString();
      var userId = Guid.NewGuid().ToString();

      #region Seed User Data
      modelBuilder.Entity<User>().HasData(
        new User { Id = adminId, UserName = "admin", UserRole = UserRole.Admin },
        new User { Id = userId, UserName = "user1", UserRole = UserRole.User }
      );
      #endregion

      #region Seed Ticket Data
      modelBuilder.Entity<Ticket>().HasData(
        new Ticket { Id = Guid.NewGuid().ToString(), UserId = adminId, Title = "Task 1", Description = "Description 1", TicketStatus = TicketStatus.Open, CreatedAt = DateTime.UtcNow },
        new Ticket { Id = Guid.NewGuid().ToString(), UserId = userId, Title = "Task 2", Description = "Description 2", TicketStatus = TicketStatus.InProgress, CreatedAt = DateTime.UtcNow }
      );
      #endregion
    }
  }
}
