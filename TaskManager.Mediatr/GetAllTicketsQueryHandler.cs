using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Console.EFCore;

namespace TaskManager.Mediatr
{
  public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, Result<IEnumerable<TicketResponse>>>
  {
    private readonly TaskManagerDbContext _dbContext;

    public GetAllTicketsQueryHandler(TaskManagerDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Result<IEnumerable<TicketResponse>>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
      try
      {
        var dbTickets = await _dbContext.Tickets.ToArrayAsync(cancellationToken: cancellationToken);

        var tickets = dbTickets.Select(ticket => new TicketResponse(
          ticket.Id,
          ticket.Title,
          ticket.Description,
          ticket.TicketStatus,
          ticket.CreatedAt,
          ticket.User?.UserName)
        );

        return Result<IEnumerable<TicketResponse>>.Success(tickets);
      }
      catch (Exception ex)
      {
        return Result<IEnumerable<TicketResponse>>.Failure(new Error(
          "GetAllTicketsQueryHandler",
          "Failed to get all tickets"
          )
        );
      }
    }
  }
}
