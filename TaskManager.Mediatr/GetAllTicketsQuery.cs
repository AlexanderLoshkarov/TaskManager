using MediatR;

namespace TaskManager.Mediatr
{
  public record GetAllTicketsQuery : IRequest<IEnumerable<TicketResponse>>;
}
