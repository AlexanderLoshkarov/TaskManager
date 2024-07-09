using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Console.EFCore
{
  public class Ticket
  {
    public required string Id { get; set; }
    public string? UserId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required TicketStatus TicketStatus { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }
  }
}
