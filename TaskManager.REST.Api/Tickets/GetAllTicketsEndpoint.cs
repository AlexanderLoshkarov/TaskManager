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
        Result<IEnumerable<TicketResponse>> result = await mediator.Send(new GetAllTicketsQuery());

        if (result.IsFailure)
        {
          return Results.BadRequest(result.Error);
        }

        return Results.Ok(result.Value.ToArray());
      });

      return app;
    }
  }
}
