using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManager.Console.EFCore;

namespace TaskManager.REST.Api.Tickets
{
  public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketResponse>>
  {
    private readonly TaskManagerDbContext _dbContext;

    public GetAllTicketsQueryHandler(TaskManagerDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<IEnumerable<TicketResponse>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
      var tickets = await _dbContext.Tickets.ToArrayAsync(cancellationToken: cancellationToken);

      return tickets.Select(ticket => new TicketResponse(
        ticket.Id,
        ticket.Title,
        ticket.Description,
        ticket.TicketStatus,
        ticket.CreatedAt,
        ticket.User?.UserName)
      ).ToArray();
    }
  }
}
