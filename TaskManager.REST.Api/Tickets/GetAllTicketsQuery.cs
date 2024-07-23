using MediatR;

namespace TaskManager.REST.Api.Tickets
{
  public class GetAllTicketsQuery : IRequest<IEnumerable<TicketResponse>>
  {
  }
}
