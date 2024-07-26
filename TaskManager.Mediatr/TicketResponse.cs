using TaskManager.Console.EFCore;

namespace TaskManager.Mediatr
{
  public record TicketResponse(
  string Id,
  string Title,
  string? Description,
  TicketStatus TicketStatus,
  DateTime CreatedAt,
  string? UserName
);
}
