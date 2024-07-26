using MediatR;
using TaskManager.Console.EFCore;

namespace TaskManager.Mediatr
{
  public class AddNewTicketRequestHandler : IRequestHandler<AddNewTicketRequest, Guid>
  {
    private readonly TaskManagerDbContext _dbContext;

    public AddNewTicketRequestHandler(TaskManagerDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Guid> Handle(AddNewTicketRequest request, CancellationToken cancellationToken)
    {
      var newId = Guid.NewGuid();
      var ticket = new Ticket
      {
        Id = newId.ToString(),
        Title = request.Title,
        Description = request.Description,
        TicketStatus = TicketStatus.New,
        CreatedAt = DateTime.UtcNow
      };

      _dbContext.Tickets.Add(ticket);
      await _dbContext.SaveChangesAsync(cancellationToken);

      return newId;
    }
  }
}
