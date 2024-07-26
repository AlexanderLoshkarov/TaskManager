using MediatR;
using TaskManager.Mediatr;

namespace TaskManager.REST.Api.Tickets
{
  public static class GetAllTicketsEndpoint
  {
    public static WebApplication MapGetAllTickets(this WebApplication app)
    {
      app.MapGet("/tickets", async (IMediator mediator) =>
      {
        IEnumerable<TicketResponse> response = await mediator.Send(new GetAllTicketsQuery());

        return response.ToArray();
      });

      return app;
    }
  }
}
