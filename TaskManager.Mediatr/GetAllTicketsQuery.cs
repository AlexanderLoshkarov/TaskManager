using MediatR;

namespace TaskManager.Mediatr
{
  public record GetAllTicketsQuery : IRequest<Result<IEnumerable<TicketResponse>>>;
}
